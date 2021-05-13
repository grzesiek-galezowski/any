using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeOrdinaryInterface<T> : IResolution<T>
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
      return typeof(T).IsInterface;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return (T)_proxyGenerator.CreateInterfaceProxyWithoutTarget(
        typeof(T), new InterfaceInterceptor(_cachedGeneration, instanceGenerator.Instance, request));
    }
  }
}
