using System;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.Interceptors;

public class FakeAbstractClass : IResolution
{
  private readonly ObjectGenerator _objectGenerator;
  private readonly CachedReturnValueGeneration _generation;
  private readonly ProxyGenerator _proxyGenerator;

  public FakeAbstractClass(
    CachedReturnValueGeneration generation, 
    ProxyGenerator proxyGenerator, 
    ObjectGenerator objectGenerator)
  {
    _generation = generation;
    _proxyGenerator = proxyGenerator;
    _objectGenerator = objectGenerator;
  }

  public bool AppliesTo(Type type)
  {
    return type.IsAbstract;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var result = _proxyGenerator.CreateClassProxy(
      type,
      _objectGenerator.GenerateConstructorParameters(instanceGenerator.Instance, request, type).ToArray(), 
      new AbstractClassInterceptor(_generation, 
        instanceGenerator.Instance, request));
    _objectGenerator.CustomizeCreatedValue(result, instanceGenerator, request, type);
      
    return result;
  }
}
