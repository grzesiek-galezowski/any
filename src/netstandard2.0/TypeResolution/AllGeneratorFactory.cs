using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;
using TddXt.TypeResolution.ResolutionChaining;

namespace TddXt.TypeResolution;

public static class AllGeneratorFactory
{
  public static BasicGenerator Create()
  {
    var dummyResolutionsFactory = new DummyResolutionsFactory(
      new EmptyCollectionInstantiation());
    var resolutionsFactory = new ResolutionsFactory(
      new ProxyGenerator(),
      new CachedReturnValueGeneration(new SynchronizedPerMethodCache<object>(new PerMethodCache<object>())),
      new SpecialCasesOfResolutions(),
      new ObjectGenerator());

    var allGenerator = new AllGenerator(new LimitedGenerationChain(
        new CustomizationSupportingChain(
          new AutoFixtureChain(
            new GeneratorsBasedChain([
              resolutionsFactory.ResolveAsHalf(),
              resolutionsFactory.ResolveAsDateOnly(),
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
              ResolutionsFactory.ResolveAsExternalOptionalOption(),
              ResolutionsFactory.ResolveAsExternalJToken(),
              ResolutionsFactory.ResolveAsExternalJContainer(),
              ResolutionsFactory.ResolveAsExternalJProperty(),
              resolutionsFactory.ResolveAsGenericEnumerator(),
              ResolutionsFactory.ResolveAsObjectEnumerator(),
              ResolutionsFactory.ResolveAsCollectionWithHeuristics(),
              resolutionsFactory.ResolveAsInterfaceImplementation(),
              resolutionsFactory.ResolveAsAbstractClassImplementation(),
              resolutionsFactory.ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(),
              ResolutionsFactory.ResolveAsVoidTask(),
              ResolutionsFactory.ResolveAsTypedTask(),
              resolutionsFactory.ResolveAsConcreteClass()
            ])))),
      new GeneratorsBasedChain(
      [
        dummyResolutionsFactory.ResolveDummyPrimitiveTypeInstance(),
          dummyResolutionsFactory.ResolveDummyString(),
          dummyResolutionsFactory.ResolveDummyOpenGenericImplementationOfIEnumerable(),
          dummyResolutionsFactory.ResolveDummyOpenGenericIEnumerable(),
          dummyResolutionsFactory.ResolveDummyAbstractType(),
          resolutionsFactory.ResolveAsInterfaceImplementation(),
          dummyResolutionsFactory.FallbackDummyObjectResolution()
      ]));
    return allGenerator;
  }
}
