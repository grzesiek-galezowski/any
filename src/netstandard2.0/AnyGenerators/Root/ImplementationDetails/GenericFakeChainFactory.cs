using System;
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
  public class GenericFakeChainFactory
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;
    private readonly Type _type;
    private readonly ISpecialCasesOfResolutions _specialCasesOfResolutions;

    public GenericFakeChainFactory(ISpecialCasesOfResolutions specialCasesOfResolutions,
      FallbackTypeGenerator fallbackTypeGenerator, 
      Type type)
    {
      _specialCasesOfResolutions = specialCasesOfResolutions;
      _fallbackTypeGenerator = fallbackTypeGenerator;
      _type = type;
    }

    public IGenerationChain NewInstance(
      CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall,
      ProxyGenerator generationIsDoneUsingProxies,
      IValueGenerator valueGenerator)
    {
      return RecursionLimited(UnconstrainedInstance(
        eachMethodReturnsTheSameValueOnEveryCall,
        generationIsDoneUsingProxies,
        valueGenerator));
    }

    public IGenerationChain UnconstrainedInstance(
      CachedReturnValueGeneration eachMethodReturnsTheSameValueOnEveryCall,
      ProxyGenerator generationIsDoneUsingProxies, 
      IValueGenerator valueGenerator)
    {
      return new TemporaryChainForCollection(_type, new[]
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

    private IResolution ResolveAsOptionalOption()
    {
      return new OptionalOptionResolution();
    }

    private IResolution ResolveAsTypedTask()
    {
      return new FakeTypedTask();
    }

    private IResolution ResolveAsVoidTask()
    {
      return new FakeVoidTask();
    }

    private IResolution ResolveAsDelegate()
    {
      return new FakeDelegate();

    }

    private IGenerationChain RecursionLimited(IGenerationChain generationChain)
    {
      return new LimitedGenerationChain(generationChain, _type);
    }

    private FakeConcreteClass ResolveAsConcreteClass()
    {
      return new FakeConcreteClass(_fallbackTypeGenerator);
    }

    private FakeConcreteClassWithNonConcreteConstructor ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
    {
      return new FakeConcreteClassWithNonConcreteConstructor(_fallbackTypeGenerator);
    }

    private FakeAbstractClass ResolveAsAbstractClassImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeAbstractClass(cachedGeneration, proxyGenerator, _fallbackTypeGenerator);
    }

    private static FakeOrdinaryInterface ResolveAsInterfaceImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
    {
      return new FakeOrdinaryInterface(cachedGeneration, proxyGenerator);
    }

    private static FakeUnknownCollection ResolveAsCollectionWithHeuristics()
    {
      return new FakeUnknownCollection();
    }

    private static FakeEnumerator ResolveAsObjectEnumerator()
    {
      return new FakeEnumerator();
    }

    private IResolution ResolveAsGenericEnumerator()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.Enumerator),
        typeof(IEnumerator<>)
      );
    }

    private IResolution ResolveAsKeyValuePair()
    {
      //todo move key value pair to inline generators
      return _specialCasesOfResolutions.CreateResolutionOfKeyValuePair();
    }

    private IResolution ResolveAsSortedDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.SortedDictionary),
        typeof(SortedDictionary<,>));
    }

    private IResolution ResolveAsConcurrentStack()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentStack),
        typeof(ConcurrentStack<>));
    }

    private IResolution ResolveAsConcurrentQueue()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentQueue),
        typeof(ConcurrentQueue<>),
        typeof(IProducerConsumerCollection<>));
    }

    private IResolution ResolveAsConcurrentBag()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ConcurrentBag),
        typeof(ConcurrentBag<>));
    }

    private IResolution ResolveAsConcurrentDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ConcurrentDictionary),
        typeof(ConcurrentDictionary<,>));
    }

    private IResolution ResolveAsSortedSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.SortedSet),
        typeof(SortedSet<>));
    }

    private IResolution ResolveAsSortedList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.SortedList),
        typeof(SortedList<,>));
    }

    private IResolution ResolveAsSimpleDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.Dictionary),
        typeof(IDictionary<,>),
        typeof(IReadOnlyDictionary<,>),
        typeof(Dictionary<,>));
    }

    private IResolution ResolveAsSimpleSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.Set),
        typeof(ISet<>),
        typeof(HashSet<>));
    }

    private IResolution ResolveAsSimpleEnumerableAndList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.List),
        typeof(IList<>),
        typeof(IEnumerable<>),
        typeof(ICollection<>),
        typeof(List<>),
        typeof(IReadOnlyList<>));
    }

    private IResolution ResolveAsImmutableQueue()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableQueue),
        typeof(ImmutableQueue<>),
        typeof(IImmutableQueue<>)
        );
    }

    private IResolution ResolveAsImmutableStack()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableStack),
        typeof(ImmutableStack<>),
        typeof(IImmutableStack<>)
        );
    }

    private IResolution ResolveAsImmutableDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ImmutableDictionary),
        typeof(ImmutableDictionary<,>),
        typeof(IImmutableDictionary<,>)
        );
    }

    private IResolution ResolveAsImmutableSortedDictionary()
    {
      return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
        nameof(InlineGenerators.ImmutableSortedDictionary),
        typeof(ImmutableSortedDictionary<,>));
    }

    private IResolution ResolveAsImmutableHashSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableHashSet),
        typeof(ImmutableHashSet<>),
        typeof(IImmutableSet<>)
      );
    }

    private IResolution ResolveAsImmutableSortedSet()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableSortedSet),
        typeof(ImmutableSortedSet<>)
      );
    }

    private IResolution ResolveAsImmutableList()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableList),
        typeof(ImmutableList<>),
        typeof(IImmutableList<>));
    }

    private IResolution ResolveAsImmutableArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
        nameof(InlineGenerators.ImmutableArray),
        typeof(ImmutableArray<>));

    }

    private static FakeSpecialCase ResolveTheMostSpecificCases(IValueGenerator valueGenerator)
    {
      return new FakeSpecialCase(valueGenerator);
    }

    private IResolution ResolveAsArray()
    {
      return _specialCasesOfResolutions.CreateResolutionOfArray();
    }
  }
}
