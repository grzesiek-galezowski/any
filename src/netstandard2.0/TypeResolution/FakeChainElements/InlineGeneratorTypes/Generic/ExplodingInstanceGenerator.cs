using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class ExplodingInstanceGenerator<T> : InlineGenerator<T> where T : class
{
  private static readonly ProxyGenerator ProxyGenerator = new();

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    if (typeof(T).IsInterface)
    {
      return ProxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new ExplodingInterceptor());
    }
    else
    {
      throw new Exception("Exploding instances can be created out of interfaces only!");
    }
  }
}
