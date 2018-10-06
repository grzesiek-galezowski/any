using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.Interceptors
{
  [Serializable]
  internal class AbstractClassInterceptor : IInterceptor
  {
    private readonly CachedReturnValueGeneration _cachedGeneration;
    private readonly Func<Type, GenerationTrace, object> _instanceSource;
    private readonly GenerationTrace _trace;

    public AbstractClassInterceptor(
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
      if (invocation.Method.IsAbstract)
      {
        _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _trace);
      }
      else if (invocation.Method.IsVirtual)
      {
        try
        {
          var previousReturnValue = invocation.ReturnValue;

          invocation.Proceed();

          if (invocation.ReturnValue == previousReturnValue)
          {
            _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _trace);
          }
        }
        catch (Exception)
        {
          _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _trace);
        }
      }
    }
  }
}