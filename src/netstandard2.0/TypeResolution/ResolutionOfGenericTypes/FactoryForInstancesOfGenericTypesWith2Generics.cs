using System;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionOfGenericTypes;

public class FactoryForInstancesOfGenericTypesWith2Generics(
  Func<Type, Type, InstanceGenerator, GenerationRequest, object> factoryMethod)
  : FactoryForInstancesOfGenericTypes
{
  public object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var typeInfo = type.GetTypeInfo();
    var type1 = typeInfo.GetGenericArguments()[0];
    var type2 = typeInfo.GetGenericArguments()[1];
    return factoryMethod.Invoke(type1, type2, instanceGenerator, request);
  }
}
