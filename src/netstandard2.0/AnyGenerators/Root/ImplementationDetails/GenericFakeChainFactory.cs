using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using Castle.DynamicProxy;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.FakeChainElements.Interceptors;
using TddXt.TypeResolution.HackedSpecialTypes;
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
      ProxyGenerator generationIsDoneUsingProxies,
      IValueGenerator valueGenerator)
    {
      return RecursionLimited(UnconstrainedInstance(
        eachMethodReturnsTheSameValueOnEveryCall,
        generationIsDoneUsingProxies,
        valueGenerator));
    }

    public IFakeChain<T> UnconstrainedInstance(CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall,
      ProxyGenerator generationIsDoneUsingProxies, IValueGenerator valueGenerator)
    {
      return new TemporaryChainForCollection<T>(new[]
      {
        ResolveTheMostSpecificCases(valueGenerator),
        ResolveAsArray(),
        ResolveAsImmutableArray(),
        ResolveAsSimpleEnumerableAndList(),
        ResolveAsImmutableList(),
        ResolveAsSimpleSet(),
        ResolveAsImmutableHashSet(),
        ResolveAsImmutableSortedSet(),
        ResolveAsSimpleDictionary(),
        ResolveAsImmutableDictionary(),
        ResolveAsImmutableSortedDictionary(),
        ResolveAsSortedList(),
        ResolveAsImmutableQueue(),
        ResolveAsImmutableStack(),
        ResolveAsDelegate(),
        ResolveAsSortedSet(),
        ResolveAsSortedDictionary(),
        ResolveAsConcurrentDictionary(),
        ResolveAsConcurrentBag(),
        ResolveAsConcurrentQueue(),
        ResolveAsConcurrentStack(),
        ResolveAsKeyValuePair(),
        ResolveAsOptionalOption(),
        ResolveAsGenericEnumerator(),
        ResolveAsObjectEnumerator(),
        ResolveAsCollectionWithHeuristics(),
        ResolveAsInterfaceImplementationWhere(
          eachMethodReturnsTheSameValueOnEveryCall,
          generationIsDoneUsingProxies),
        ResolveAsAbstractClassImplementationWhere(
          eachMethodReturnsTheSameValueOnEveryCall,
          generationIsDoneUsingProxies),
        ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature(),
        ResolveAsVoidTask(),
        ResolveAsTypedTask(),
        ResolveAsConcreteClass()
      });
    }

    private IResolution<T> ResolveAsOptionalOption()
    {
      return new OptionalOptionResolution<T>();
    }

    private IResolution<T> ResolveAsTypedTask()
    {
      return new FakeTypedTask<T>();
    }

    private IResolution<T> ResolveAsVoidTask()
    {
      return new FakeVoidTask<T>();
    }

    private IResolution<T> ResolveAsDelegate()
    {
      return new FakeDelegate<T>();

    }

    private static IFakeChain<T> RecursionLimited(IFakeChain<T> fakeChain)
    {
      return new LimitedFakeChain<T>(fakeChain);
    }

    private FakeConcreteClass<T> ResolveAsConcreteClass()
    {
      return new FakeConcreteClass<T>(_fallbackTypeGenerator);
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

    private IResolution<T> ResolveAsImmutableQueue()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableQueue),
        typeof(ImmutableQueue<>),
        typeof(IImmutableQueue<>)
        );
    }

    private IResolution<T> ResolveAsImmutableStack()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableStack),
        typeof(ImmutableStack<>),
        typeof(IImmutableStack<>)
        );
    }

    private IResolution<T> ResolveAsImmutableDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ImmutableDictionary),
        typeof(ImmutableDictionary<,>),
        typeof(IImmutableDictionary<,>)
        );
    }

    private IResolution<T> ResolveAsImmutableSortedDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ImmutableSortedDictionary),
        typeof(ImmutableSortedDictionary<,>));
    }

    private IResolution<T> ResolveAsImmutableHashSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableHashSet),
        typeof(ImmutableHashSet<>),
        typeof(IImmutableSet<>)
      );
    }

    private IResolution<T> ResolveAsImmutableSortedSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableSortedSet),
        typeof(ImmutableSortedSet<>)
      );
    }

    private IResolution<T> ResolveAsImmutableList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableList),
        typeof(ImmutableList<>),
        typeof(IImmutableList<>));
    }

    private IResolution<T> ResolveAsImmutableArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableArray),
        typeof(ImmutableArray<>));

    }

    private static FakeSpecialCase<T> ResolveTheMostSpecificCases(IValueGenerator valueGenerator)
    {
      return new FakeSpecialCase<T>(valueGenerator);
    }

    private IResolution<T> ResolveAsArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOfArray();
    }
  }
}