using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeUnknownCollection<T> : IResolution<T>
  {
    public bool Applies()
    {
      var isCollection = TypeOf<T>.IsImplementationOfOpenGeneric(typeof (IProducerConsumerCollection<>))
               || TypeOf<T>.IsImplementationOfOpenGeneric(typeof(ICollection<>));
      return TypeOf<T>.IsConcrete() &&
             typeof (T).IsGenericType &&
             isCollection &&
             TypeOf<T>.HasParameterlessConstructor();

    }


    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var collectionType = typeof (T);
      var collectionInstance = Activator.CreateInstance(collectionType);
      var elementTypes = collectionType.GetGenericArguments();

      var addMethod = collectionType.GetMethod("Add", elementTypes)
        ?? collectionType.GetMethod("TryAdd", elementTypes)
        ?? collectionType.GetMethod("Push", elementTypes)
        ?? collectionType.GetMethod("Enqueue", elementTypes);

      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator, trace));
      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator, trace));
      addMethod.Invoke(
        collectionInstance, 
        AnyInstancesOf(elementTypes, instanceGenerator, trace));

      return (T) collectionInstance;
    }

    private static object[] AnyInstancesOf(IEnumerable<Type> elementTypes, InstanceGenerator instanceGenerator,
      GenerationTrace trace)
    {
      return elementTypes.Select(type => instanceGenerator.Instance(type, trace)).ToArray();
    }
  }
}