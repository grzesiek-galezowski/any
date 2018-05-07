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
      var emptyCollectionFixture = new Fixture
      {
        RepeatCount = 0
      };
      var methodProxyCalls = new GenericMethodProxyCalls();
      var valueGenerator = CreateValueGenerator();
      var numericGenerator = new NumericGenerator();

      var emptyCollectionGenerator = new EmptyCollectionInstantiation();
      var proxyGenerator = new ProxyGenerator();
      var proxyBasedGenerator = new ProxyBasedGenerator(
        emptyCollectionFixture, 
        methodProxyCalls, 
        emptyCollectionGenerator, //TODO make separate chain for this
        proxyGenerator, 
        new FakeChainFactory(
          new CachedReturnValueGeneration(new PerMethodCache<object>()), 
          GlobalNestingLimit.Of(5), 
          proxyGenerator, //TODO get rid of this dependency - its runtime-circular
          valueGenerator),
        valueGenerator);

      var allGenerator = new AllGenerator(valueGenerator, 
        proxyBasedGenerator);
      return allGenerator;
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