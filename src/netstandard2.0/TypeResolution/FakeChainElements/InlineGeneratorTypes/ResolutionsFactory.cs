using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using Castle.DynamicProxy;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public class ResolutionsFactory
{
  private readonly ProxyGenerator _proxyFactory;
  private readonly CachedReturnValueGeneration _cachedGeneration;
  private readonly ISpecialCasesOfResolutions _specialCasesOfResolutions;
  private readonly ObjectGenerator _objectGenerator;

  public ResolutionsFactory(
    ProxyGenerator proxyFactory, 
    CachedReturnValueGeneration cachedGeneration,
    ISpecialCasesOfResolutions specialCasesOfResolutions,
    ObjectGenerator objectGenerator)
  {
    _proxyFactory = proxyFactory;
    _cachedGeneration = cachedGeneration;
    _specialCasesOfResolutions = specialCasesOfResolutions;
    _objectGenerator = objectGenerator;
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
    return new FakeConcreteClass(_objectGenerator);
  }

  public FakeConcreteClassWithNonConcreteConstructor ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
  {
    return new FakeConcreteClassWithNonConcreteConstructor(_objectGenerator);
  }

  public FakeAbstractClass ResolveAsAbstractClassImplementation()
  {
    return new FakeAbstractClass(_cachedGeneration, _proxyFactory, _objectGenerator);
  }

  public FakeOrdinaryInterface ResolveAsInterfaceImplementation()
  {
    return new FakeOrdinaryInterface(_cachedGeneration, _proxyFactory);
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
      nameof(InternalInlineGenerators.Enumerator),
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
      nameof(InternalInlineGenerators.SortedDictionary),
      typeof(SortedDictionary<,>));
  }

  public IResolution ResolveAsConcurrentStack()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentStack),
      typeof(ConcurrentStack<>));
  }

  public IResolution ResolveAsConcurrentQueue()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentQueue),
      typeof(ConcurrentQueue<>),
      typeof(IProducerConsumerCollection<>));
  }

  public IResolution ResolveAsConcurrentBag()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentBag),
      typeof(ConcurrentBag<>));
  }

  public IResolution ResolveAsConcurrentDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ConcurrentDictionary),
      typeof(ConcurrentDictionary<,>));
  }

  public IResolution ResolveAsSortedSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.SortedSet),
      typeof(SortedSet<>));
  }

  public IResolution ResolveAsSortedList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.SortedList),
      typeof(SortedList<,>));
  }

  public IResolution ResolveAsSimpleDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.Dictionary),
      typeof(IDictionary<,>),
      typeof(IReadOnlyDictionary<,>),
      typeof(Dictionary<,>));
  }

  public IResolution ResolveAsSimpleSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Set),
      typeof(ISet<>),
      typeof(HashSet<>));
  }

  public IResolution ResolveAsSimpleEnumerableAndList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.List),
      typeof(IList<>),
      typeof(IEnumerable<>),
      typeof(ICollection<>),
      typeof(List<>),
      typeof(IReadOnlyList<>));
  }

  public IResolution ResolveAsImmutableQueue()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableQueue),
      typeof(ImmutableQueue<>),
      typeof(IImmutableQueue<>)
    );
  }

  public IResolution ResolveAsImmutableStack()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableStack),
      typeof(ImmutableStack<>),
      typeof(IImmutableStack<>)
    );
  }

  public IResolution ResolveAsImmutableDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ImmutableDictionary),
      typeof(ImmutableDictionary<,>),
      typeof(IImmutableDictionary<,>)
    );
  }

  public IResolution ResolveAsImmutableSortedDictionary()
  {
    return _specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ImmutableSortedDictionary),
      typeof(ImmutableSortedDictionary<,>));
  }

  public IResolution ResolveAsImmutableHashSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableHashSet),
      typeof(ImmutableHashSet<>),
      typeof(IImmutableSet<>)
    );
  }

  public IResolution ResolveAsImmutableSortedSet()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableSortedSet),
      typeof(ImmutableSortedSet<>)
    );
  }

  public IResolution ResolveAsImmutableList()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableList),
      typeof(ImmutableList<>),
      typeof(IImmutableList<>));
  }

  public IResolution ResolveAsImmutableArray()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableArray),
      typeof(ImmutableArray<>));

  }

  public static TypeObjectsGenerator ResolveAsType()
  {
    return new TypeObjectsGenerator();
  }

  public static IResolution ResolveAsMethodInfo()
  {
    return new MethodInfoGenerator();
  }

  public IResolution ResolveAsArray()
  {
    return _specialCasesOfResolutions.CreateResolutionOfArray();
  }

  public IResolution ResolveAsUri()
  {
    return new UriResolution();
  }

  public static ExceptionGenerator ResolveAsException()
  {
    return new ExceptionGenerator();
  }

  public IResolution ResolveAsLazy()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Lazy),
      typeof(Lazy<>));

  }

  public IResolution ResolveAsNullable()
  {
    return _specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Nullable),
      typeof(Nullable<>));
  }

  public IResolution ResolveAsCultureInfo()
  {
    return new CultureInfoResolution();
  }

  public IResolution ResolveAsDateOnly()
  {
    return new DateOnlyResolution();
  }

  public IResolution ResolveAsHalf()
  {
    return new HalfResolution();
  }
}
