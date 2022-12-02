using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeUnknownCollection : IResolution
{
  private static readonly (Type type, string addMethod)[] SupportedTypes =
  {
    (typeof(IProducerConsumerCollection<>), nameof(IProducerConsumerCollection<object>.TryAdd)),
    (typeof(ICollection<>), nameof(ICollection<object>.Add))
  };

  public bool AppliesTo(Type type)
  {
    var smartType = SmartType.For(type);
    var isCollection = SupportedTypes.Any(tuple => smartType.IsImplementationOfOpenGeneric(tuple.type));
    return smartType.IsConcrete() &&
           isCollection &&
           smartType.HasPublicParameterlessConstructor() &&
           IsNotExplicitlyExcluded(smartType);

  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type typeOfCollection)
  {
    var collectionInstance = Activator.CreateInstance(typeOfCollection);
    var smartTypeOfCollection = SmartType.For(typeOfCollection);

    foreach (var (supportedType, addMethodName) in SupportedTypes)
    {
      if (smartTypeOfCollection.IsImplementationOfOpenGeneric(supportedType))
      {
        FillCollectionInstance(
          instanceGenerator,
          request,
          smartTypeOfCollection,
          collectionInstance,
          supportedType,
          addMethodName);
        return collectionInstance;
      }
    }

    throw new InvalidOperationException("The type " + typeOfCollection +
                                        " is not supported by custom collection generator");
  }

  private static void FillCollectionInstance(
    InstanceGenerator instanceGenerator,
    GenerationRequest request,
    IType smartTypeOfCollection,
    object collectionInstance,
    Type openGenericType,
    string addMethodName)
  {
    var collectionType = smartTypeOfCollection.FindInterfacesForOpenGenericDefinition(openGenericType).Single();
    var elementTypes = collectionType.GenericTypeArguments;
    var addMethod = collectionType.GetMethod(addMethodName, elementTypes).OrThrow();
    
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));
  }

  private static object[] AnyInstancesOf(IEnumerable<Type> elementTypes, InstanceGenerator instanceGenerator,
    GenerationRequest request)
  {
    return elementTypes.Select(type => instanceGenerator.Instance(type, request)).ToArray();
  }

  private bool IsNotExplicitlyExcluded(ISmartType smartType)
  {
    return !smartType.IsFromNamespace("Newtonsoft.Json.Linq");
  }

}
