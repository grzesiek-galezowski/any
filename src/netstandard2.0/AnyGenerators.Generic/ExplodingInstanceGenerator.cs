using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.Interceptors;

namespace TddXt.AnyGenerators.Generic
{
  public class ExplodingInstanceGenerator<T> : InlineGenerator<T> where T : class
  {
    private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace) 
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
}