using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements;

//bug refactor parts of this code to special collection-like class with methods like .Add() or .IsEmpty()
public static class MutableCollectionFiller
{
  private static readonly (Type type, string addMethod)[] SupportedCollectionTypes =
  [
    (typeof(IProducerConsumerCollection<>), nameof(IProducerConsumerCollection<object>.TryAdd)),
    (typeof(ICollection<>), nameof(ICollection<object>.Add))
  ];

  public static void Fill(object collectionInstance, InstanceGenerator instanceGenerator,
    GenerationRequest request)
  {
    var smartTypeOfCollection = SmartType.For(collectionInstance.GetType());

      foreach (var (supportedType, addMethodName) in SupportedCollectionTypes)
      {
        if (smartTypeOfCollection.IsImplementationOfOpenGeneric(supportedType))
        {
          Fill(
            collectionInstance,
            addMethodName,
            supportedType,
            instanceGenerator,
            request,
            smartTypeOfCollection);
          return;
        }
      }

      throw new InvalidOperationException("The type " + collectionInstance.GetType() +
                                          " is not supported by custom collection generator");
  }

  public static bool IsSupported(Type type)
  {
    return IsSupported(SmartType.For(type));
  }

  private static object[] AnyInstancesOf(
    IEnumerable<Type> elementTypes, 
    InstanceGenerator instanceGenerator,
    GenerationRequest request)
  {
    return elementTypes.Select(type => instanceGenerator.Instance(type, request)).ToArray();
  }

  private static void Fill(object collectionInstance,
    string addMethodName,
    Type openGenericType,
    InstanceGenerator instanceGenerator,
    GenerationRequest request,
    IType smartTypeOfCollection)
  {
    var collectionType = smartTypeOfCollection.FindInterfacesForOpenGenericDefinition(openGenericType).Single();
    var elementTypes = collectionType.GenericTypeArguments;
    var addMethod = collectionType.GetMethod(addMethodName, elementTypes).OrThrow();

    for (int i = 0; i < request.Many; i++)
    {
      addMethod.Invoke(
        collectionInstance, AnyInstancesOf(elementTypes, instanceGenerator, request));
    }
  }

  public static bool IsEmpty(object collectionInstance)
  {
    //if collection is empty, then the first MoveNext of iterator should return false
    return !((IEnumerable)collectionInstance).GetEnumerator().MoveNext();
  }

  private static bool IsNotExplicitlyExcluded(ISmartType smartType)
  {
    return !(
      //some Newtonsoft.Json classes implement collection interfaces but behave in a very specific way,
      //making filling them with using any kind of common heuristics next to impossible 
      smartType.IsFromNamespace("Newtonsoft.Json.Linq") || 
      
      //immutable collections cannot be added to without replacing the object
      smartType.IsFromNamespace("System.Collections.Immutable") || 
      
      //arrays cannot be added to without replacing the object
      smartType.IsArray()
      );
  }

  private static bool IsCollection(IType smartType)
  {
    var isCollection =
      SupportedCollectionTypes.Any(tuple => smartType.IsImplementationOfOpenGeneric(tuple.type));
    return isCollection;
  }

  private static bool IsSupported(ISmartType smartType)
  {
    return IsNotExplicitlyExcluded(smartType) && IsCollection(smartType);
  }
}
