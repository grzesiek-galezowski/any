using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution;

[Serializable]
public class CachedReturnValueGeneration
{
  private readonly PerMethodCache<object> _cache;

  public CachedReturnValueGeneration(PerMethodCache<object> cache)
  {
    _cache = cache;
  }

  public void SetupReturnValueFor(
    IInvocation invocation,
    Func<Type, GenerationRequest, object> instanceSource,
    GenerationRequest request)
  {
    var interceptedInvocation = new InterceptedInvocation(invocation, instanceSource);
    if (interceptedInvocation.HasReturnValue())
    {
      interceptedInvocation.GenerateAndAddMethodReturnValueTo(_cache, request);
    }
    else if (interceptedInvocation.IsPropertySetter())
    {
      interceptedInvocation.GenerateAndAddPropertyGetterReturnValueTo(_cache);
    }

  }
}