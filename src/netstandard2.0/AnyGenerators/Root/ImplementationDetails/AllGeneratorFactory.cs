using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.AutoFixtureWrapper;
using TddXt.AnyGenerators.Generic;
using TddXt.TypeResolution;
using TddXt.TypeResolution.CustomCollections;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

//bug DefaultRelays
//bug DefaultPrimitiveBuilders
//bug yield return (ISpecimenBuilder) new LazyRelay();
//bug yield return (ISpecimenBuilder) new MultidimensionalArrayRelay();
//bug yield return (ISpecimenBuilder) new ArrayRelay();
//bug yield return (ISpecimenBuilder) new ParameterRequestRelay();
//bug yield return (ISpecimenBuilder) new PropertyRequestRelay();
//bug yield return (ISpecimenBuilder) new FieldRequestRelay();
//bug yield return (ISpecimenBuilder) new RangedSequenceRelay();
//bug yield return (ISpecimenBuilder) new FiniteSequenceRelay();
//bug yield return (ISpecimenBuilder) new SeedIgnoringRelay();
//bug yield return (ISpecimenBuilder) new MethodInvoker((IMethodQuery) new CompositeMethodQuery(new IMethodQuery[2]

public static class AllGeneratorFactory
{
  public static BasicGenerator Create()
  {
    var valueGenerator = CreateValueGenerator();
    var proxyGenerator = new ProxyGenerator();
    var cachedReturnValueGeneration = new CachedReturnValueGeneration(new PerMethodCache<object>());
    var specialCasesOfResolutions = new SpecialCasesOfResolutions();
    var fallbackTypeGenerator = new FallbackTypeGenerator(
      new IFallbackGeneratedObjectCustomization[]
      {
        new FillPropertiesCustomization(),
        new FillFieldsCustomization()
      });
    var resolutionsFactory = new ResolutionsFactory(
      specialCasesOfResolutions, fallbackTypeGenerator);
    var unconstrainedChain = new AutoFixtureChain(
      new TemporaryChainForCollection(new[]
      {
        ResolutionsFactory.ResolveTheMostSpecificCases(valueGenerator),
        resolutionsFactory.ResolveAsUri(),
        resolutionsFactory.ResolveAsArray(),
        resolutionsFactory.ResolveAsImmutableArray(),
        resolutionsFactory.ResolveAsSimpleEnumerableAndList(),
        resolutionsFactory.ResolveAsImmutableList(),
        resolutionsFactory.ResolveAsSimpleSet(),
        resolutionsFactory.ResolveAsImmutableHashSet(),
        resolutionsFactory.ResolveAsImmutableSortedSet(),
        resolutionsFactory.ResolveAsSimpleDictionary(),
        resolutionsFactory.ResolveAsImmutableDictionary(),
        resolutionsFactory.ResolveAsImmutableSortedDictionary(),
        resolutionsFactory.ResolveAsSortedList(),
        resolutionsFactory.ResolveAsImmutableQueue(),
        resolutionsFactory.ResolveAsImmutableStack(),
        ResolutionsFactory.ResolveAsDelegate(),
        resolutionsFactory.ResolveAsSortedSet(),
        resolutionsFactory.ResolveAsSortedDictionary(),
        resolutionsFactory.ResolveAsConcurrentDictionary(),
        resolutionsFactory.ResolveAsConcurrentBag(),
        resolutionsFactory.ResolveAsConcurrentQueue(),
        resolutionsFactory.ResolveAsConcurrentStack(),
        resolutionsFactory.ResolveAsKeyValuePair(),
        ResolutionsFactory.ResolveAsOptionalOption(),
        resolutionsFactory.ResolveAsGenericEnumerator(),
        ResolutionsFactory.ResolveAsObjectEnumerator(),
        ResolutionsFactory.ResolveAsCollectionWithHeuristics(),
        ResolutionsFactory.ResolveAsInterfaceImplementationWhere(cachedReturnValueGeneration, proxyGenerator),
        resolutionsFactory.ResolveAsAbstractClassImplementationWhere(cachedReturnValueGeneration, proxyGenerator),
        resolutionsFactory.ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(),
        ResolutionsFactory.ResolveAsVoidTask(),
        ResolutionsFactory.ResolveAsTypedTask(),
        resolutionsFactory.ResolveAsConcreteClass()
      }));
    var limitedGenerationChain = new LimitedGenerationChain(unconstrainedChain);
    var fakeOrdinaryInterfaceGenerator = new FakeOrdinaryInterface(cachedReturnValueGeneration, proxyGenerator);

    var allGenerator = new AllGenerator(
      limitedGenerationChain, 
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
