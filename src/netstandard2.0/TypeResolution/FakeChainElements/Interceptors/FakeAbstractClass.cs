using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors
{
  public class FakeAbstractClass : IResolution //bug done
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private readonly CachedReturnValueGeneration _generation;
    private readonly ProxyGenerator _proxyGenerator;

    public FakeAbstractClass(
      CachedReturnValueGeneration generation, 
      ProxyGenerator proxyGenerator, 
      FallbackTypeGenerator fallbackTypeGenerator)
    {
      _generation = generation;
      _proxyGenerator = proxyGenerator;
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool AppliesTo(Type type)
    {
      return type.IsAbstract;
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      var result = _proxyGenerator.CreateClassProxy(
        type,
        _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator.Instance, request).ToArray(), 
        new AbstractClassInterceptor(_generation, 
          instanceGenerator.Instance, request));
      _fallbackTypeGenerator.CustomizeCreatedValue(result, instanceGenerator, request);
      
      return result;
    }
  }
}
