using Castle.DynamicProxy;
using AutoFixture;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.TypeResolution;
using TddEbook.TddToolkit.TypeResolution.CustomCollections;
using TddEbook.TddToolkit.TypeResolution.FakeChainElements;

namespace TddEbook.TddToolkit.Subgenerators
{
  public static class AllGeneratorFactory
  {
    public static AllGenerator Create()
    {
      var methodProxyCalls = new GenericMethodProxyCalls();
      var valueGenerator = CreateValueGenerator();
      var proxyGenerator = new ProxyGenerator();
      var fakeChainFactory = CreateFakeChainFactory(proxyGenerator, valueGenerator);

      var allGenerator = new AllGenerator(valueGenerator, fakeChainFactory, methodProxyCalls);
      return allGenerator;
    }

    public static FakeChainFactory CreateFakeChainFactory(ProxyGenerator proxyGenerator, ValueGenerator valueGenerator)
    {
      return new FakeChainFactory(
        new CachedReturnValueGeneration(new PerMethodCache<object>()), 
        GlobalNestingLimit.Of(5), 
        proxyGenerator,
        valueGenerator);
    }

    public static ValueGenerator CreateValueGenerator()
    {
      var fixtureConfiguration = new AutoFixtureConfiguration();
      var fixture = fixtureConfiguration.CreateUnconfiguredInstance();
      var valueGenerator = new ValueGenerator(fixture);
      fixtureConfiguration.ApplyTo(fixture);
      return valueGenerator;
    }
  }
}