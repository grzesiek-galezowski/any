using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeUnknownCollection : IResolution
{
  public bool AppliesTo(Type type)
  {
    var smartType = SmartType.For(type);
    var isCollection = smartType.IsImplementationOfOpenGeneric(typeof(IProducerConsumerCollection<>))
                       || smartType.IsImplementationOfOpenGeneric(typeof(ICollection<>));
    return smartType.IsConcrete() &&
           isCollection &&
           smartType.HasPublicParameterlessConstructor();

  }


  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type typeOfCollection)
  {
    var collectionInstance = Activator.CreateInstance(typeOfCollection);
    var smartTypeOfCollection = SmartType.For(typeOfCollection);
    if(smartTypeOfCollection.IsImplementationOfOpenGeneric(typeof(IProducerConsumerCollection<>)))
    {
      FillCollectionInstance(
        instanceGenerator,
        request,
        smartTypeOfCollection,
        collectionInstance,
        typeof(IProducerConsumerCollection<>),
        nameof(IProducerConsumerCollection<object>.TryAdd));
    }
    else if (smartTypeOfCollection.IsImplementationOfOpenGeneric(typeof(ICollection<>)))
    {
      FillCollectionInstance(
        instanceGenerator,
        request,
        smartTypeOfCollection,
        collectionInstance,
        typeof(ICollection<>),
        nameof(ICollection<object>.Add));
    }
    else
    {
      throw new InvalidOperationException("The type " + typeOfCollection +
                                          " is not supported by custom collection generator");
    }

    return collectionInstance;
  }

  private static object FillCollectionInstance(
    InstanceGenerator instanceGenerator,
    GenerationRequest request,
    IType smartTypeOfCollection,
    object collectionInstance,
    Type openGenericType,
    string addMethodName)
  {
    var collectionType = smartTypeOfCollection.FindInterfacesForOpenGenericDefinition(openGenericType).Single();
    var elementTypes = collectionType.GenericTypeArguments;
    var addMethod = collectionType.GetMethod(addMethodName, elementTypes);
    
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));
    addMethod.Invoke(
      collectionInstance,
      AnyInstancesOf(elementTypes, instanceGenerator, request));

    return collectionInstance;
  }

  private static object[] AnyInstancesOf(IEnumerable<Type> elementTypes, InstanceGenerator instanceGenerator,
    GenerationRequest request)
  {
    return elementTypes.Select(type => instanceGenerator.Instance(type, request)).ToArray();
  }
}
