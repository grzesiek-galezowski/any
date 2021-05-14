using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeUnknownCollection : IResolution
  {
    public bool AppliesTo(Type type)
    {
      var smartType = SmartType.For(type);
      var isCollection = smartType.IsImplementationOfOpenGeneric(typeof(IProducerConsumerCollection<>))
                         || smartType.IsImplementationOfOpenGeneric(typeof(ICollection<>));
      return smartType.IsConcrete() &&
             type.IsGenericType &&
             isCollection &&
             smartType.HasPublicParameterlessConstructor();

    }


    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type typeOfCollection)
    {
      var collectionInstance = Activator.CreateInstance(typeOfCollection);
      var elementTypes = typeOfCollection.GetGenericArguments();

      var addMethod = typeOfCollection.GetMethod("Add", elementTypes)
        ?? typeOfCollection.GetMethod("TryAdd", elementTypes)
        ?? typeOfCollection.GetMethod("Push", elementTypes)
        ?? typeOfCollection.GetMethod("Enqueue", elementTypes);

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
}
