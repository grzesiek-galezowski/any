using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeOrdinaryInterface : IResolution
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly ProxyGenerator _proxyGenerator;

    public FakeOrdinaryInterface(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      _cachedGeneration = cachedGeneration;
      _proxyGenerator = proxyGenerator;
    }

    public bool AppliesTo(Type type)
    {
      return type.IsInterface;
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return _proxyGenerator.CreateInterfaceProxyWithoutTarget(
        type, new InterfaceInterceptor(_cachedGeneration, instanceGenerator.Instance, request));
    }
  }
}
