using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

[Serializable]
internal class AbstractClassInterceptor : IInterceptor
{
  private readonly CachedReturnValueGeneration _cachedGeneration;
  private readonly Func<Type, GenerationRequest, object> _instanceSource;
  private readonly GenerationRequest _request;

  public AbstractClassInterceptor(
    CachedReturnValueGeneration cachedGeneration,
    Func<Type, GenerationRequest, object> instanceSource,
    GenerationRequest request)
  {
    _cachedGeneration = cachedGeneration;
    _instanceSource = instanceSource;
    _request = request;
  }

  public void Intercept(IInvocation invocation)
  {
    NSubstituteHacks.AssertIsNotInvokedDuringNSubstituteQuery(invocation, _instanceSource);

    if (invocation.Method.IsAbstract)
    {
      _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _request);
    }
    else if (invocation.Method.IsVirtual)
    {
      try
      {
        var previousReturnValue = invocation.ReturnValue;

        invocation.Proceed();

        if (invocation.ReturnValue == previousReturnValue)
        {
          _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _request);
        }
      }
      catch (Exception)
      {
        _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _request);
      }
    }
  }
}