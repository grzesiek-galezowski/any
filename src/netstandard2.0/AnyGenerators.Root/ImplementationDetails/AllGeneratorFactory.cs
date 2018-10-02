using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.AutoFixtureWrapper;
using TddXt.TypeResolution;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public static class AllGeneratorFactory
  {
    public static BasicGenerator Create()
    {
      var methodProxyCalls = new GenericMethodProxyCalls();
      var valueGenerator = CreateValueGenerator();
      var proxyGenerator = new ProxyGenerator();
      var fakeChainFactory = CreateFakeChainFactory(proxyGenerator, valueGenerator);

      var allGenerator = new SynchronizedBasicGenerator(new AllGenerator(valueGenerator, fakeChainFactory, methodProxyCalls));
      return allGenerator;
    }

    private static IFakeChainFactory CreateFakeChainFactory(ProxyGenerator proxyGenerator, ValueGenerator valueGenerator)
    {
      return new FakeChainFactory(
        new CachedReturnValueGeneration(new PerMethodCache<object>()), 
        GlobalNestingLimit.Of(5), 
        proxyGenerator,
        valueGenerator);
    }

    private static ValueGenerator CreateValueGenerator()
    {
      var fixtureWrapper = FixtureWrapper.CreateUnconfiguredInstance();
      var fixtureConfiguration = new AutoFixtureConfiguration();
      fixtureConfiguration.ApplyTo(fixtureWrapper);
      
      var valueGenerator = new ValueGenerator(fixtureWrapper);
      return valueGenerator;
    }
  }

  public class SynchronizedBasicGenerator : BasicGenerator
  {
    private static readonly object SyncRoot = new object();
    private readonly AllGenerator _allGenerator;

    public SynchronizedBasicGenerator(AllGenerator allGenerator)
    {
      _allGenerator = allGenerator;
    }

    public T Instance<T>()
    {
      lock (SyncRoot)
      {
        return _allGenerator.Instance<T>();
      }
    }

    public T Instance<T>(params GenerationCustomization[] customizations)
    {
      lock (SyncRoot)
      {
        return _allGenerator.Instance<T>(customizations);
      }
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      lock (SyncRoot)
      {
        return _allGenerator.InstanceOf(gen);
      }
    }
  }
}