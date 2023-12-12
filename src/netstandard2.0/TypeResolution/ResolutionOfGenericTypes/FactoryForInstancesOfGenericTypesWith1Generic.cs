using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionOfGenericTypes;

public class FactoryForInstancesOfGenericTypesWith1Generic(
  Func<Type, InstanceGenerator, GenerationRequest, object> factoryMethod)
  : FactoryForInstancesOfGenericTypes
{
  public object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var type1 = type.GetTypeInfo().GetGenericArguments()[0];
    return factoryMethod.Invoke(type1, instanceGenerator, request);
  }
}
