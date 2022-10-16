using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeResolution;
using TddXt.TypeResolution.CustomCollections;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.FakeChainElements.DummyChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

public static class AllGeneratorFactory
{
  public static BasicGenerator Create()
  {
    var proxyGenerator = new ProxyGenerator();
    var cachedReturnValueGeneration = new CachedReturnValueGeneration(new PerMethodCache<object>());
    var specialCasesOfResolutions = new SpecialCasesOfResolutions();
    var fallbackTypeGenerator = new ObjectGenerator(
      new IFallbackGeneratedObjectCustomization[]
      {
        new FillPropertiesCustomization(), 
        new FillFieldsCustomization()
      });
    var resolutionsFactory = new ResolutionsFactory(
      specialCasesOfResolutions, fallbackTypeGenerator);
    var limitedGenerationChain = new LimitedGenerationChain(
      new CustomizationSupportingChain(
        new AutoFixtureChain(
          new GenereatorsBasedChain(new[]
          {
            resolutionsFactory.ResolveAsNullable(),
            resolutionsFactory.ResolveAsCultureInfo(),
            resolutionsFactory.ResolveAsLazy(),
            ResolutionsFactory.ResolveAsException(),
            ResolutionsFactory.ResolveAsMethodInfo(),
            ResolutionsFactory.ResolveAsType(),
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
          }))));

    var emptyCollectionInstantiation = new EmptyCollectionInstantiation();
    
    //bug create a separate factory for these objects
    var generatorsBasedChain = new GenereatorsBasedChain(
      new IResolution[]
      {
        new ResolveDummyPrimitiveTypeInstance(), 
        new ResolveDummyString(),
        new ResolveDummyOpenGenericImplementationOfIEnumerable(emptyCollectionInstantiation),
        new ResolveDummyOpenGenericIEnumerable(emptyCollectionInstantiation),
        new ResolveDummyAbstractType(),
        ResolutionsFactory.ResolveAsInterfaceImplementationWhere(cachedReturnValueGeneration, proxyGenerator),
        new FallbackDummyObjectResolution()
      });
    var allGenerator = new AllGenerator(limitedGenerationChain, generatorsBasedChain);
    return allGenerator;
  }
}
