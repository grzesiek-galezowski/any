using System;
using System.Collections.Concurrent;
using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeChainFactory : IFakeChainFactory
  {
    private readonly CachedReturnValueGeneration _cachedReturnValueGeneration;
    private readonly ConcurrentDictionary<Type, object> _constrainedFactoryCache = new();//new MemoryCache("constrained");
    private readonly ProxyGenerator _proxyGenerator;
    private readonly ConcurrentDictionary<Type, object> _unconstrainedFactoryCache = new();//new MemoryCache("constrained");
    private readonly ValueGenerator _valueGenerator;
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private readonly GenericFakeChainFactory _genericFakeChainFactory;

    public FakeChainFactory(
      CachedReturnValueGeneration cachedReturnValueGeneration,
      ProxyGenerator proxyGenerator,
      ValueGenerator valueGenerator)
    {
      _cachedReturnValueGeneration = cachedReturnValueGeneration;
      _proxyGenerator = proxyGenerator;
      _valueGenerator = valueGenerator;
      _fallbackTypeGenerator = new FallbackTypeGenerator(
        new IFallbackGeneratedObjectCustomization[]
        {
          new FillPropertiesCustomization(),
          new FillFieldsCustomization()
        });
      _genericFakeChainFactory = CreateGenericFakeChainFactory();
    }

    public IGenerationChain GetInstance<T>(Type type)
    {
      return GetInstanceWithMemoization(type, () =>
        _genericFakeChainFactory.NewInstance(
          _cachedReturnValueGeneration,
          _proxyGenerator,
          _valueGenerator), 
        _constrainedFactoryCache);
    }

    public IGenerationChain GetUnconstrainedInstance(Type type)
    {
      return GetInstanceWithMemoization(type, () =>
      _genericFakeChainFactory
        .UnconstrainedInstance(
          _cachedReturnValueGeneration,
          _proxyGenerator, 
          _valueGenerator),
          _unconstrainedFactoryCache);
    }

    public IResolution CreateFakeOrdinaryInterfaceGenerator()
    {
      //bug this doesn't fit 100% here.
      return new FakeOrdinaryInterface(_cachedReturnValueGeneration, _proxyGenerator);
    }

    private static IGenerationChain GetInstanceWithMemoization(
      Type key, 
      Func<IGenerationChain> func,
      ConcurrentDictionary<Type, object> cache)
    {
      if (!cache.TryGetValue(key, out var outVal))
      {
        var newInstance = func.Invoke();
        cache[key] = newInstance;
        return newInstance;
      }

      return (IGenerationChain)outVal;
    }

    private GenericFakeChainFactory CreateGenericFakeChainFactory()
    {
      return new GenericFakeChainFactory(
        new SpecialCasesOfResolutions(),
        _fallbackTypeGenerator);
    }
  }
}
