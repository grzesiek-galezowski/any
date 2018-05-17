using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TypeResolution;
using TypeResolution.CustomCollections;

namespace Generators.ImplementationDetails
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

    private static FakeChainFactory CreateFakeChainFactory(ProxyGenerator proxyGenerator, ValueGenerator valueGenerator)
    {
      return new FakeChainFactory(
        new CachedReturnValueGeneration(new PerMethodCache<object>()), 
        GlobalNestingLimit.Of(5), 
        proxyGenerator,
        valueGenerator);
    }

    private static ValueGenerator CreateValueGenerator()
    {
      var fixtureConfiguration = new AutoFixtureConfiguration();
      var fixture = fixtureConfiguration.CreateUnconfiguredInstance();
      var valueGenerator = new ValueGenerator(fixture);
      fixtureConfiguration.ApplyTo(fixture);
      return valueGenerator;
    }
  }
}