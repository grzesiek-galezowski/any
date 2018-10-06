using System;
using System.Collections.Concurrent;
using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeReflection;
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

    public IFakeChain<T> GetInstance<T>()
    {
      return GetInstanceWithMemoization(() => 
        CreateGenericFakeChainFactory<T>().NewInstance(
            _cachedReturnValueGeneration,
            _nestingLimit,
            _proxyGenerator,
            _valueGenerator
          ), _constrainedFactoryCache);
    }

    public IFakeChain<T> GetUnconstrainedInstance<T>()
    {
      return GetInstanceWithMemoization(() => 
      CreateGenericFakeChainFactory<T>()
        .UnconstrainedInstance(
          _cachedReturnValueGeneration,
          _proxyGenerator, _valueGenerator), 
          _unconstrainedFactoryCache);
    }

    public ISpecialCasesOfResolutions<T> CreateSpecialCasesOfResolutions<T>()
    {
      return new SpecialCasesOfResolutions<T>();
    }

    public IResolution<T> CreateFakeOrdinaryInterfaceGenerator<T>()
    {
      //bug this doesn't fit 100% here.
      return new FakeOrdinaryInterface<T>(
        _cachedReturnValueGeneration, _proxyGenerator);
    }

    private static IFakeChain<T> GetInstanceWithMemoization<T>(Func<IFakeChain<T>> func, ConcurrentDictionary<Type, object> cache)
    {
      var key = typeof(T);

      if(!cache.TryGetValue(key, out var outVal))
      {
        var newInstance = func.Invoke();
        cache[key] = newInstance;
        return newInstance;
      }

      return (IFakeChain<T>) outVal;
    }

    private GenericFakeChainFactory<T> CreateGenericFakeChainFactory<T>()
    {
      return new GenericFakeChainFactory<T>(CreateSpecialCasesOfResolutions<T>(), new FallbackTypeGenerator<T>(new FallbackTypeGenerator(
        new IFallbackGeneratedObjectCustomization[]
        {
          new FillPropertiesCustomization(),
          new FillFieldsCustomization()
        }, SmartType.For(typeof(T)))));
    }
  }
}