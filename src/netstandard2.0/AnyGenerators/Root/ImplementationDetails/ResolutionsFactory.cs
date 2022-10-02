using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using AutoFixture;
using AutoFixture.Kernel;
using Castle.DynamicProxy;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.FakeChainElements.Interceptors;
using TddXt.TypeResolution.HackedSpecialTypes;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

public class ResolutionsFactory
{
  private readonly ISpecialCasesOfResolutions _specialCasesOfResolutions;
  private readonly FallbackTypeGenerator _fallbackTypeGenerator;

  public ResolutionsFactory(
    ISpecialCasesOfResolutions specialCasesOfResolutions, 
    FallbackTypeGenerator fallbackTypeGenerator)
  {
    _specialCasesOfResolutions = specialCasesOfResolutions;
    _fallbackTypeGenerator = fallbackTypeGenerator;
  }

  public static IResolution ResolveAsOptionalOption()
  {
    return new OptionalOptionResolution();
  }

  public static IResolution ResolveAsTypedTask()
  {
    return new FakeTypedTask();
  }

  public static IResolution ResolveAsVoidTask()
  {
    return new FakeVoidTask();
  }

  public static IResolution ResolveAsDelegate()
  {
    return new FakeDelegate();
  }

  public IResolution ResolveAsConcreteClass()
  {
    return new FakeConcreteClass(_fallbackTypeGenerator);
  }

  public FakeConcreteClassWithNonConcreteConstructor ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
  {
    return new FakeConcreteClassWithNonConcreteConstructor(_fallbackTypeGenerator);
  }

  public FakeAbstractClass ResolveAsAbstractClassImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
  {
    return new FakeAbstractClass(cachedGeneration, proxyGenerator, _fallbackTypeGenerator);
  }

  public static FakeOrdinaryInterface ResolveAsInterfaceImplementationWhere(CachedReturnValueGeneration cachedGeneration, ProxyGenerator proxyGenerator)
  {
    return new FakeOrdinaryInterface(cachedGeneration, proxyGenerator);
  }

  public static FakeUnknownCollection ResolveAsCollectionWithHeuristics()
  {
    return new FakeUnknownCollection();
  }

  public static FakeEnumerator ResolveAsObjectEnumerator()
  {
    return new FakeEnumerator();
  }

  public IResolution ResolveAsGenericEnumerator()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.Enumerator),
      typeof(IEnumerator<>)
    );
  }

  public IResolution ResolveAsKeyValuePair()
  {
    //todo move key value pair to inline generators
    return _specialCasesOfResolutions.CreateResolutionOfKeyValuePair();
  }

  public IResolution ResolveAsSortedDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.SortedDictionary),
      typeof(SortedDictionary<,>));
  }

  public IResolution ResolveAsConcurrentStack()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ConcurrentStack),
      typeof(ConcurrentStack<>));
  }

  public IResolution ResolveAsConcurrentQueue()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ConcurrentQueue),
      typeof(ConcurrentQueue<>),
      typeof(IProducerConsumerCollection<>));
  }

  public IResolution ResolveAsConcurrentBag()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ConcurrentBag),
      typeof(ConcurrentBag<>));
  }

  public IResolution ResolveAsConcurrentDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.ConcurrentDictionary),
      typeof(ConcurrentDictionary<,>));
  }

  public IResolution ResolveAsSortedSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.SortedSet),
      typeof(SortedSet<>));
  }

  public IResolution ResolveAsSortedList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.SortedList),
      typeof(SortedList<,>));
  }

  public IResolution ResolveAsSimpleDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.Dictionary),
      typeof(IDictionary<,>),
      typeof(IReadOnlyDictionary<,>),
      typeof(Dictionary<,>));
  }

  public IResolution ResolveAsSimpleSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.Set),
      typeof(ISet<>),
      typeof(HashSet<>));
  }

  public IResolution ResolveAsSimpleEnumerableAndList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.List),
      typeof(IList<>),
      typeof(IEnumerable<>),
      typeof(ICollection<>),
      typeof(List<>),
      typeof(IReadOnlyList<>));
  }

  public IResolution ResolveAsImmutableQueue()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableQueue),
      typeof(ImmutableQueue<>),
      typeof(IImmutableQueue<>)
    );
  }

  public IResolution ResolveAsImmutableStack()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableStack),
      typeof(ImmutableStack<>),
      typeof(IImmutableStack<>)
    );
  }

  public IResolution ResolveAsImmutableDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.ImmutableDictionary),
      typeof(ImmutableDictionary<,>),
      typeof(IImmutableDictionary<,>)
    );
  }

  public IResolution ResolveAsImmutableSortedDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InlineGenerators.ImmutableSortedDictionary),
      typeof(ImmutableSortedDictionary<,>));
  }

  public IResolution ResolveAsImmutableHashSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableHashSet),
      typeof(ImmutableHashSet<>),
      typeof(IImmutableSet<>)
    );
  }

  public IResolution ResolveAsImmutableSortedSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableSortedSet),
      typeof(ImmutableSortedSet<>)
    );
  }

  public IResolution ResolveAsImmutableList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableList),
      typeof(ImmutableList<>),
      typeof(IImmutableList<>));
  }

  public IResolution ResolveAsImmutableArray()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InlineGenerators.ImmutableArray),
      typeof(ImmutableArray<>));

  }

  public static FakeSpecialCase ResolveTheMostSpecificCases(IValueGenerator valueGenerator)
  {
    return new FakeSpecialCase(valueGenerator);
  }

  public IResolution ResolveAsArray()
  {
    return _specialCasesOfResolutions.CreateResolutionOfArray();
  }

  public IResolution ResolveAsUri()
  {
    return new UriResolution();
  }
}
