using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

[Serializable]
public class InterfaceInterceptor(
  CachedReturnValueGeneration cachedGeneration,
  Func<Type, GenerationRequest, object> instanceSource,
  GenerationRequest request)
  : IInterceptor
{
  private readonly CachedReturnValueGeneration _cachedGeneration = cachedGeneration;
  private readonly Func<Type, GenerationRequest, object> _instanceSource = instanceSource;
  private readonly GenerationRequest _request = request;

  public void Intercept(IInvocation invocation)
  {
    NSubstituteHacks.AssertIsNotInvokedDuringNSubstituteQuery(invocation, _instanceSource);
    _cachedGeneration.SetupReturnValueFor(invocation, _instanceSource, _request);
  }
}
