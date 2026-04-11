using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

public class FakeAbstractClass(
  CachedReturnValueGeneration generation,
  ProxyGenerator proxyGenerator,
  ObjectGenerator objectGenerator)
  : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type.IsAbstract;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var constructorArguments = objectGenerator.GenerateConstructorParameters(instanceGenerator.Instance, request, type).ToArray();
    var result = proxyGenerator.CreateClassProxy(
      type,
      constructorArguments, 
      new AbstractClassInterceptor(generation, 
        instanceGenerator.Instance, request));
    request.CustomizeCreatedValue(result, instanceGenerator);
      
    return result;
  }
}
