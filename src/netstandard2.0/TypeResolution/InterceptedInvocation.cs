using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution
{
  public class InterceptedInvocation
  {
    private readonly Func<Type, GenerationRequest, object> _instanceSource;
    private readonly IInvocation _invocation;

    public InterceptedInvocation(
      IInvocation invocation,
      Func<Type, GenerationRequest, object> instanceSource)
    {
      _invocation = invocation;
      _instanceSource = instanceSource;
    }

    public bool HasReturnValue()
    {
      return _invocation.Method.ReturnType != typeof(void);
    }

    public bool IsPropertySetter()
    {
      return _invocation.Method.DeclaringType.GetProperties()
        .Any(prop => prop.GetSetMethod() == _invocation.Method);
    }

    public bool IsPropertyGetter()
    {
      return _invocation.Method.DeclaringType.GetProperties()
        .Any(prop => prop.GetGetMethod() == _invocation.Method);
    }


    private PerMethodCacheKey GetPropertyGetterCacheKey()
    {
      var propertyFromSetterCallOrNull =
        _invocation.Method.GetPropertyFromSetterCall();
      var getter = propertyFromSetterCallOrNull.GetGetMethod(true);
      var key = PerMethodCacheKey.For(getter, _invocation.Proxy);
      return key;
    }

    public void GenerateAndAddPropertyGetterReturnValueTo(PerMethodCache<object> perMethodCache)
    {
      var key = GetPropertyGetterCacheKey();
      perMethodCache.Overwrite(key, _invocation.Arguments[0]);
    }

    public void GenerateAndAddMethodReturnValueTo(PerMethodCache<object> perMethodCache, GenerationRequest request)
    {
      var cacheKey = PerMethodCacheKey.For(_invocation);
      if (!perMethodCache.AlreadyContainsValueFor(cacheKey))
      {
        var returnValue = AnyInstanceOfReturnTypeOf(_invocation, request);
        perMethodCache.Add(cacheKey, returnValue);
        _invocation.ReturnValue = returnValue;
      }
      else
      {
        _invocation.ReturnValue = perMethodCache.ValueFor(cacheKey);
      }
    }

    private object AnyInstanceOfReturnTypeOf(IInvocation invocation, GenerationRequest request)
    {
      return _instanceSource(invocation.Method.ReturnType, request);
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

      var properties = call.DeclaringType.GetProperties();
      return properties.FirstOrDefault(x => x.GetSetMethod() == call);
    }

    private static bool CanBePropertySetterCall(MethodInfo call)
    {
      return call.IsSpecialName && call.Name.StartsWith("set_", StringComparison.Ordinal);
    }
  }
}