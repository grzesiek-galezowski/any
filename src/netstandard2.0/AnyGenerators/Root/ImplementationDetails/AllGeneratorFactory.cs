using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.AutoFixtureWrapper;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeResolution;
using TddXt.TypeResolution.CustomCollections;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public static class AllGeneratorFactory
  {
    public static BasicGenerator Create()
    {
      var methodProxyCalls = new GenericMethodProxyCalls();
      var valueGenerator = CreateValueGenerator();
      var proxyGenerator = new ProxyGenerator();
      var cachedReturnValueGeneration = new CachedReturnValueGeneration(new PerMethodCache<object>());
      var fakeChainFactory = new GenericFakeChainFactory(
        new SpecialCasesOfResolutions(),
        new FallbackTypeGenerator(
          new IFallbackGeneratedObjectCustomization[]
          {
            new FillPropertiesCustomization(),
            new FillFieldsCustomization()
          }));
      var generationChain = fakeChainFactory.NewInstance(
        cachedReturnValueGeneration,
        proxyGenerator,
        valueGenerator);
      var unconstrainedChain = fakeChainFactory.UnconstrainedInstance(
        cachedReturnValueGeneration,
        proxyGenerator, 
        valueGenerator);
      var fakeOrdinaryInterfaceGenerator =
        new FakeOrdinaryInterface(cachedReturnValueGeneration, proxyGenerator);

      var allGenerator = new AllGenerator(
        valueGenerator, 
        methodProxyCalls, 
        generationChain, 
        unconstrainedChain,
        fakeOrdinaryInterfaceGenerator);
      return allGenerator;
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
}
