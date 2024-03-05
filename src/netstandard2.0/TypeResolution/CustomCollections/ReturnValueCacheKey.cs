using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace TddXt.TypeResolution.CustomCollections;

[Serializable]
public record PerMethodCacheKey
{
#pragma warning disable IDE0052 // Remove unread private members
  private readonly MethodInfo _method;
  private readonly object _proxy;
#pragma warning restore IDE0052 // Remove unread private members

  private PerMethodCacheKey(MethodInfo method, object proxy)
  {
    _method = method;
    _proxy = proxy;
  }

  public static PerMethodCacheKey For(IInvocation invocation)
  {
    return new PerMethodCacheKey(invocation.Method, invocation.Proxy);
  }

  public static PerMethodCacheKey For(MethodInfo method, object proxy)
  {
    return new PerMethodCacheKey(method, proxy);
  }
}
