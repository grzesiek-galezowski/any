using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeOrdinaryInterface(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
  : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type.IsInterface;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return proxyGenerator.CreateInterfaceProxyWithoutTarget(
      type, new InterfaceInterceptor(cachedGeneration, instanceGenerator.Instance, request));
  }
}
