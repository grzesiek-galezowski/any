using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FactoryForInstancesOfGenericTypesWith2Generics : FactoryForInstancesOfGenericTypes
  {
    private readonly Func<Type, Type, InstanceGenerator, GenerationTrace, object> _factoryMethod;

    public FactoryForInstancesOfGenericTypesWith2Generics(
      Func<Type, Type, InstanceGenerator, GenerationTrace, object> factoryMethod)
    {
      _factoryMethod = factoryMethod;
    }

    public object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var typeInfo = type.GetTypeInfo();
      var type1 = typeInfo.GetGenericArguments()[0];
      var type2 = typeInfo.GetGenericArguments()[1];
      return _factoryMethod.Invoke(type1, type2, instanceGenerator, trace);
    }
  }
}