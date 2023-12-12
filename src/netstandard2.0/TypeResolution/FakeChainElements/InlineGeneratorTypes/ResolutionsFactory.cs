using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using Castle.DynamicProxy;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public class ResolutionsFactory(
  ProxyGenerator proxyFactory,
  CachedReturnValueGeneration cachedGeneration,
  ISpecialCasesOfResolutions specialCasesOfResolutions,
  ObjectGenerator objectGenerator)
{
  public static IResolution ResolveAsExternalOptionalOption()
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
    return new FakeConcreteClass(objectGenerator);
  }

  public FakeConcreteClassWithNonConcreteConstructor ResolveAsConcreteTypeWithNonConcreteTypesInConstructorSignature()
  {
    return new FakeConcreteClassWithNonConcreteConstructor(objectGenerator);
  }

  public FakeAbstractClass ResolveAsAbstractClassImplementation()
  {
    return new FakeAbstractClass(cachedGeneration, proxyFactory, objectGenerator);
  }

  public FakeOrdinaryInterface ResolveAsInterfaceImplementation()
  {
    return new FakeOrdinaryInterface(cachedGeneration, proxyFactory);
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
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Enumerator),
      typeof(IEnumerator<>)
    );
  }

  public IResolution ResolveAsKeyValuePair()
  {
    //todo move key value pair to inline generators
    return specialCasesOfResolutions.CreateResolutionOfKeyValuePair();
  }

  public IResolution ResolveAsSortedDictionary()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.SortedDictionary),
      typeof(SortedDictionary<,>));
  }

  public IResolution ResolveAsConcurrentStack()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentStack),
      typeof(ConcurrentStack<>));
  }

  public IResolution ResolveAsConcurrentQueue()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentQueue),
      typeof(ConcurrentQueue<>),
      typeof(IProducerConsumerCollection<>));
  }

  public IResolution ResolveAsConcurrentBag()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ConcurrentBag),
      typeof(ConcurrentBag<>));
  }

  public IResolution ResolveAsConcurrentDictionary()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ConcurrentDictionary),
      typeof(ConcurrentDictionary<,>));
  }

  public IResolution ResolveAsSortedSet()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.SortedSet),
      typeof(SortedSet<>));
  }

  public IResolution ResolveAsSortedList()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.SortedList),
      typeof(SortedList<,>));
  }

  public IResolution ResolveAsSimpleDictionary()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.Dictionary),
      typeof(IDictionary<,>),
      typeof(IReadOnlyDictionary<,>),
      typeof(Dictionary<,>));
  }

  public IResolution ResolveAsSimpleSet()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Set),
      typeof(ISet<>),
      typeof(HashSet<>));
  }

  public IResolution ResolveAsSimpleEnumerableAndList()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.List),
      typeof(IList<>),
      typeof(IEnumerable<>),
      typeof(ICollection<>),
      typeof(List<>),
      typeof(IReadOnlyList<>));
  }

  public IResolution ResolveAsImmutableQueue()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableQueue),
      typeof(ImmutableQueue<>),
      typeof(IImmutableQueue<>)
    );
  }

  public IResolution ResolveAsImmutableStack()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableStack),
      typeof(ImmutableStack<>),
      typeof(IImmutableStack<>)
    );
  }

  public IResolution ResolveAsImmutableDictionary()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ImmutableDictionary),
      typeof(ImmutableDictionary<,>),
      typeof(IImmutableDictionary<,>)
    );
  }

  public IResolution ResolveAsImmutableSortedDictionary()
  {
    return specialCasesOfResolutions.CreateResolutionOf2GenericType(
      nameof(InternalInlineGenerators.ImmutableSortedDictionary),
      typeof(ImmutableSortedDictionary<,>));
  }

  public IResolution ResolveAsImmutableHashSet()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableHashSet),
      typeof(ImmutableHashSet<>),
      typeof(IImmutableSet<>)
    );
  }

  public IResolution ResolveAsImmutableSortedSet()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableSortedSet),
      typeof(ImmutableSortedSet<>)
    );
  }

  public IResolution ResolveAsImmutableList()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.ImmutableList),
      typeof(ImmutableList<>),
      typeof(IImmutableList<>));
  }

  public IResolution ResolveAsImmutableArray()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
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
    return specialCasesOfResolutions.CreateResolutionOfArray();
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
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
      nameof(InternalInlineGenerators.Lazy),
      typeof(Lazy<>));

  }

  public IResolution ResolveAsNullable()
  {
    return specialCasesOfResolutions.CreateResolutionOf1GenericType(
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

  public static IResolution ResolveAsExternalJToken()
  {
    return new JTokenResolution();
  }

  public static IResolution ResolveAsExternalJContainer()
  {
    return new JContainerResolution();
  }

  public static IResolution ResolveAsExternalJProperty()
  {
    return new JPropertyResolution();
  }
}
