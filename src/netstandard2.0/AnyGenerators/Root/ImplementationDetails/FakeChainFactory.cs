using System;
using System.Collections.Concurrent;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeChainFactory : IFakeChainFactory
  {
    private readonly CachedReturnValueGeneration _cachedReturnValueGeneration;
    private readonly ConcurrentDictionary<Type, object> _constrainedFactoryCache = new ConcurrentDictionary<Type, object>();//new MemoryCache("constrained");
    private readonly NestingLimit _nestingLimit;
    private readonly ProxyGenerator _proxyGenerator;
    private readonly ConcurrentDictionary<Type, object> _unconstrainedFactoryCache = new ConcurrentDictionary<Type, object>();//new MemoryCache("constrained");
    private readonly ValueGenerator _valueGenerator;

    public FakeChainFactory(
      CachedReturnValueGeneration cachedReturnValueGeneration,
      NestingLimit nestingLimit,
      ProxyGenerator proxyGenerator,
      ValueGenerator valueGenerator)
    {
      _cachedReturnValueGeneration = cachedReturnValueGeneration;
      _nestingLimit = nestingLimit;
      _proxyGenerator = proxyGenerator;
      _valueGenerator = valueGenerator;
    }

    public IGenerationChain GetInstance<T>(Type type)
    {
      return GetInstanceWithMemoization(type, () =>
        CreateGenericFakeChainFactory(type).NewInstance(
          _cachedReturnValueGeneration,
          _proxyGenerator,
          _valueGenerator
        ), _constrainedFactoryCache);
    }

    public IGenerationChain GetUnconstrainedInstance(Type type)
    {
      return GetInstanceWithMemoization(type, () =>
      CreateGenericFakeChainFactory(type)
        .UnconstrainedInstance(
          _cachedReturnValueGeneration,
          _proxyGenerator, 
          _valueGenerator),
          _unconstrainedFactoryCache);
    }

    public ISpecialCasesOfResolutions CreateSpecialCasesOfResolutions()
    {
      return new SpecialCasesOfResolutions();
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

    private GenericFakeChainFactory CreateGenericFakeChainFactory(Type type)
    {
      var fallbackTypeGenerator = new FallbackTypeGenerator(
        new IFallbackGeneratedObjectCustomization[]
        {
          new FillPropertiesCustomization(),
          new FillFieldsCustomization()
        });
      return new GenericFakeChainFactory(
        CreateSpecialCasesOfResolutions(),
        fallbackTypeGenerator,
        type);
    }
  }
}
