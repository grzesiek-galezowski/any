using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FactoryForInstancesOfGenericTypesWith1Generic : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, InstanceGenerator, GenerationTrace, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith1Generic(
      Func<Type, InstanceGenerator, GenerationTrace, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var type1 = type.GetTypeInfo().GetGenericArguments()[0];
      return _factoryMethod.Invoke(type1, instanceGenerator, trace);
    }
  }
}