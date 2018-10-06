using System.Collections.Concurrent;
using System.Collections.Generic;
using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.Interceptors;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class GenericFakeChainFactory<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;
    private readonly ISpecialCasesOfResolutions<T> _specialCasesOfResolutions;

    public GenericFakeChainFactory(ISpecialCasesOfResolutions<T> specialCasesOfResolutions, FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      _specialCasesOfResolutions = specialCasesOfResolutions;
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public IFakeChain<T> NewInstance(
      CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall,
      NestingLimit nestingLimit,
      ProxyGenerator generationIsDoneUsingProxies,
      IValueGenerator valueGenerator)
    {
      return LimitedTo(nestingLimit, UnconstrainedInstance(
        eachMethodReturnsTheSameValueOnEveryCall,
        generationIsDoneUsingProxies,
        valueGenerator));
    }

    public FakeChain<T> UnconstrainedInstance(CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall,
      ProxyGenerator generationIsDoneUsingProxies, IValueGenerator valueGenerator)
    {
      return OrderedChainOfGenerationsWithTheFollowingLogic(TryTo(
        ResolveTheMostSpecificCases(valueGenerator),
        ElseTryTo(ResolveAsArray(),
          ElseTryTo(ResolveAsSimpleEnumerableAndList(),
            ElseTryTo(ResolveAsSimpleSet(),
              ElseTryTo(ResolveAsSimpleDictionary(),
                ElseTryTo(ResolveAsSortedList(),
                  ElseTryTo(ResolveAsDelegate(),
                    ElseTryTo(ResolveAsSortedSet(),
                      ElseTryTo(ResolveAsSortedDictionary(),
                        ElseTryTo(ResolveAsConcurrentDictionary(),
                          ElseTryTo(ResolveAsConcurrentBag(),
                            ElseTryTo(ResolveAsConcurrentQueue(),
                              ElseTryTo(ResolveAsConcurrentStack(),
                                ElseTryTo(ResolveAsKeyValuePair(),
                                  ElseTryTo(ResolveAsGenericEnumerator(),
                                    ElseTryTo(ResolveAsObjectEnumerator(),
                                      ElseTryTo(ResolveAsCollectionWithHeuristics(),
                                        ElseTryTo(ResolveAsInterfaceImplementationWhere(eachMethodReturnsTheSameValueOnEveryCall, generationIsDoneUsingProxies),
                                          ElseTryTo(ResolveAsAbstractClassImplementationWhere(eachMethodReturnsTheSameValueOnEveryCall, generationIsDoneUsingProxies),
                                            ElseTryTo(ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(),
                                              ElseTryTo(ResolveAsConcreteClass(valueGenerator),
                                                ElseReportUnsupportedType()
                                              ))))))))))))))))))))));
    }

    private IResolution<T> ResolveAsDelegate()
    {
      return new FakeDelegate<T>();

    }

    private static FakeChain<T> OrderedChainOfGenerationsWithTheFollowingLogic(IChainElement<T> first)
    {
      return new FakeChain<T>(first);
    }

    private static IFakeChain<T> LimitedTo(NestingLimit limit, IFakeChain<T> fakeChain)
    {
      return new LimitedFakeChain<T>(limit, fakeChain);
    }

    private FakeConcreteClass<T> ResolveAsConcreteClass(IValueGenerator valueGenerator)
    {
      return new FakeConcreteClass<T>(
        _fallbackTypeGenerator, valueGenerator);
    }

    private FakeConcreteClassWithNonConcreteConstructor<T> ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
    {
      return new FakeConcreteClassWithNonConcreteConstructor<T>(
        _fallbackTypeGenerator);
    }

    private FakeAbstractClass<T> ResolveAsAbstractClassImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeAbstractClass<T>(cachedGeneration, proxyGenerator, _fallbackTypeGenerator);
    }

    private static FakeOrdinaryInterface<T> ResolveAsInterfaceImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeOrdinaryInterface<T>(cachedGeneration, proxyGenerator);
    }

    private static FakeUnknownCollection<T> ResolveAsCollectionWithHeuristics()
    {
      return new FakeUnknownCollection<T>();
    }

    private static FakeEnumerator<T> ResolveAsObjectEnumerator()
    {
      return new FakeEnumerator<T>();
    }

    private IResolution<T> ResolveAsGenericEnumerator()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.Enumerator),
        typeof(IEnumerator<>)
      );
    }

    private IResolution<T> ResolveAsKeyValuePair()
    {
      //todo move key value pair to inline generators
      return _specialCasesOfResolutions.CreateResolutionOfKeyValuePair();
    }

    private IResolution<T> ResolveAsSortedDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.SortedDictionary),
        typeof(SortedDictionary<,>));
    }

    private IResolution<T> ResolveAsConcurrentStack()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentStack),
        typeof(ConcurrentStack<>));
    }

    private IResolution<T> ResolveAsConcurrentQueue()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentQueue),
        typeof(ConcurrentQueue<>),
        typeof(IProducerConsumerCollection<>));
    }

    private IResolution<T> ResolveAsConcurrentBag()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentBag),
        typeof(ConcurrentBag<>));
    }

    private IResolution<T> ResolveAsConcurrentDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ConcurrentDictionary),
        typeof(ConcurrentDictionary<,>));
    }

    private IResolution<T> ResolveAsSortedSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.SortedSet),
        typeof(SortedSet<>));
    }

    private IResolution<T> ResolveAsSortedList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.SortedList),
        typeof(SortedList<,>));
    }

    private IResolution<T> ResolveAsSimpleDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.Dictionary),
        typeof(IDictionary<,>),
        typeof(IReadOnlyDictionary<,>),
        typeof(Dictionary<,>));
    }

    private IResolution<T> ResolveAsSimpleSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.Set),
        typeof(ISet<>),
        typeof(HashSet<>));
    }

    private IResolution<T> ResolveAsSimpleEnumerableAndList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.List),
        typeof(IList<>),
        typeof(IEnumerable<>),
        typeof(ICollection<>),
        typeof(List<>),
        typeof(IReadOnlyList<>));
    }

    private static FakeSpecialCase<T> ResolveTheMostSpecificCases(IValueGenerator valueGenerator)
    {
      return new FakeSpecialCase<T>(valueGenerator);
    }

    private static InvalidChainElement<T> ElseReportUnsupportedType()
    {
      return new InvalidChainElement<T>();
    }

    private static ChainElement<T> ElseTryTo(IResolution<T> handleArraysInSpecialWay, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(handleArraysInSpecialWay, chainElement);
    }

    private static IChainElement<T> TryTo(IResolution<T> fakeSpecialCase, IChainElement<T> chainElement)
    {
      return new ChainElement<T>(fakeSpecialCase, chainElement);
    }

    private IResolution<T> ResolveAsArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOfArray();
    }
  }

}