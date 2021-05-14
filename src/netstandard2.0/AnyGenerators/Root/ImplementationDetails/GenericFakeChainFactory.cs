using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class GenericFakeChainFactory //bug continue here!!!
  {
    private readonly IGenerationChain _generationChain;
    private readonly TemporaryChainForCollection _unconstrainedChain;

    public GenericFakeChainFactory(
      ISpecialCasesOfResolutions specialCasesOfResolutions,
      CachedReturnValueGeneration cachedReturnValueGeneration,
      ProxyGenerator proxyGenerator,
      ValueGenerator valueGenerator,
      FallbackTypeGenerator fallbackTypeGenerator)
    {
      var resolutionsFactory = new ResolutionsFactory(
        specialCasesOfResolutions, fallbackTypeGenerator);
      _unconstrainedChain = new TemporaryChainForCollection(new[]
      {
        ResolutionsFactory.ResolveTheMostSpecificCases(valueGenerator),
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
        ResolutionsFactory.ResolveAsInterfaceImplementationWhere(
          cachedReturnValueGeneration, 
          proxyGenerator),
        resolutionsFactory.ResolveAsAbstractClassImplementationWhere(
          cachedReturnValueGeneration, 
          proxyGenerator),
        resolutionsFactory.ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(), 
        ResolutionsFactory.ResolveAsVoidTask(), 
        ResolutionsFactory.ResolveAsTypedTask(), 
        resolutionsFactory.ResolveAsConcreteClass()
      });
      _generationChain = new LimitedGenerationChain(_unconstrainedChain);
    }

    public IGenerationChain NewInstance()
    {
      return _generationChain;
    }

    public IGenerationChain UnconstrainedInstance()
    {
      return _unconstrainedChain;
    }
  }
}
