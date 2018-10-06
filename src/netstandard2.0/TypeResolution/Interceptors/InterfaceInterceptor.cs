using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.Interceptors
{
  [Serializable]
  public class InterfaceInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly Func<Type, GenerationTrace, object> _instanceSource;
    private readonly GenerationTrace _trace;

    public InterfaceInterceptor(
      CachedReturnValueGeneration cachedGeneration, 
      Func<Type, GenerationTrace, object> instanceSource, 
      GenerationTrace trace)
    {
      _cachedGeneration = cachedGeneration;
      _instanceSource = instanceSource;
      _trace = trace;
    }

    public void Intercept(IInvocation invocation)
    {
      _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _trace);
    }
  }
}