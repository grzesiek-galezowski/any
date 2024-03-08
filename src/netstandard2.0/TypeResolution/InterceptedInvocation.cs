using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution;

public class InterceptedInvocation(
  IInvocation invocation,
  Func<Type, GenerationRequest, object> instanceSource)
  : IInterceptedInvocation
{
  public bool HasReturnValue()
  {
    return invocation.Method.ReturnType != typeof(void);
  }

  public bool IsPropertySetter()
  {
    var methodDeclaringType = invocation.Method.DeclaringType;

    return methodDeclaringType != null && 
           methodDeclaringType.GetProperties()
             .Any(prop => prop.GetSetMethod() == invocation.Method);
  }

  public bool IsPropertyGetter()
  {
    var methodDeclaringType = invocation.Method.DeclaringType;
    return methodDeclaringType != null && methodDeclaringType.GetProperties()
      .Any(prop => prop.GetGetMethod() == invocation.Method);
  }

  private Maybe<PerMethodCacheKey> GetPropertyGetterCacheKey()
  {
    var propertyFromSetterCallOrNull =
      invocation.Method.GetPropertyFromSetterCall();
    var key = propertyFromSetterCallOrNull.GetGetMethod(true)
      .ToMaybe().Select(g => PerMethodCacheKey.For(g, invocation.Proxy)); //BUG: this will return null sometimes
    return key;
  }

  public void GenerateAndAddPropertyGetterReturnValueTo(IPerMethodCache<object> perMethodCache)
  {
    var key = GetPropertyGetterCacheKey();
    if (key.HasValue)
    {
      perMethodCache.Overwrite(key.Value(), invocation.Arguments[0]);
    }
  }

  public void GenerateAndAddMethodReturnValueTo(IPerMethodCache<object> perMethodCache, GenerationRequest request)
  {
    var cacheKey = PerMethodCacheKey.For(invocation);
    perMethodCache.AddIfNoValueFor(cacheKey, () => AnyInstanceOfReturnTypeOf(invocation, request));
    invocation.ReturnValue = perMethodCache.ValueFor(cacheKey);
  }

  private object AnyInstanceOfReturnTypeOf(IInvocation invocation, GenerationRequest request)
  {
    return instanceSource(invocation.Method.ReturnType, request);
  }

}

public static class ReflectionExtensions
{
  public static PropertyInfo GetPropertyFromSetterCall(this MethodInfo call)
  {
    if (!CanBePropertySetterCall(call))
    {
      throw new Exception("property not settable");
    }

    var properties = call.DeclaringType.OrThrow().GetProperties();
    return properties.FirstOrDefault(x => x.GetSetMethod() == call).OrThrow();
  }

  private static bool CanBePropertySetterCall(MethodInfo call)
  {
    return call.IsSpecialName && call.Name.StartsWith("set_", StringComparison.Ordinal);
  }
}
