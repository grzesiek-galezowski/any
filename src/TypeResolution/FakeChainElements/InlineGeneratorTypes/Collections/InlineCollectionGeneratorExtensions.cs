using System;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public static class InlineCollectionGeneratorExtensions
{
  extension<T>(InlineGenerator<IEnumerable<T>> enumerableGenerator)
  {
    //todo these methods can be further generified. Leaving for now to see what syntax is needed

    public EnumerableConversion<T, T[]> AsArray()
    {
      return Conversion(enumerableGenerator, Enumerable.ToArray);
    }

    public EnumerableConversion<T, List<T>> AsList()
    {
      return Conversion(enumerableGenerator, Enumerable.ToList);
    }

    public EnumerableConversion<T, IReadOnlyList<T>> AsReadOnlyList()
    {
      Func<IEnumerable<T>, IReadOnlyList<T>> x = Enumerable.ToList;
      return Conversion(enumerableGenerator, x);
    }

    public InlineGenerator<HashSet<T>> AsHashSet()
    {
      return Conversion(enumerableGenerator, enumerable => new HashSet<T>(enumerable));
    }

    public EnumerableConversion<T, ISet<T>> AsSet()
    {
      return Conversion(enumerableGenerator, enumerable => (ISet<T>)new HashSet<T>(enumerable));
    }

    public EnumerableConversion<T, SortedSet<T>> AsSortedSet()
    {
      return Conversion(enumerableGenerator, enumerable => new SortedSet<T>(enumerable));
    }

    public EnumerableConversion<T, ConcurrentStack<T>> AsConcurrentStack()
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentStack<T>(enumerable));
    }

    public EnumerableConversion<T, ConcurrentQueue<T>> AsConcurrentQueue()
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentQueue<T>(enumerable));
    }

    public EnumerableConversion<T, ConcurrentBag<T>> AsConcurrentBag()
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentBag<T>(enumerable));
    }

    public EnumerableConversion<T, ImmutableArray<T>> AsImmutableArray()
    {
      return Conversion(enumerableGenerator, ImmutableArray.ToImmutableArray);
    }

    public EnumerableConversion<T, ImmutableList<T>> AsImmutableList()
    {
      return Conversion(enumerableGenerator, ImmutableList.ToImmutableList);
    }

    public InlineGenerator<ImmutableHashSet<T>> AsImmutableHashSet()
    {
      return Conversion(enumerableGenerator, ImmutableHashSet.ToImmutableHashSet);
    }

    public InlineGenerator<ImmutableSortedSet<T>> AsImmutableSortedSet()
    {
      return Conversion(enumerableGenerator, ImmutableSortedSet.ToImmutableSortedSet);
    }

    public InlineGenerator<FrozenSet<T>> AsFrozenSet()
    {
      return Conversion(enumerableGenerator, enumerable => enumerable.ToFrozenSet());
    }

    public InlineGenerator<ImmutableQueue<T>> AsImmutableQueue()
    {
      return Conversion(enumerableGenerator, ImmutableQueue.CreateRange);
    }

    public InlineGenerator<ImmutableStack<T>> AsImmutableStack()
    {
      return Conversion(enumerableGenerator, ImmutableStack.CreateRange);
    }
  }

  extension<TKey, TValue>(InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator) where TKey : notnull
  {
    public EnumerableConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>>
    AsDictionary()
    {
      return Conversion(enumerableGenerator,
        enumerable => enumerable.ToDictionary(x => x.Key, x => x.Value));
    }

    public EnumerableConversion<KeyValuePair<TKey, TValue>, FrozenDictionary<TKey, TValue>>
      AsFrozenDictionary()
    {
      return Conversion(enumerableGenerator,
        enumerable => enumerable.ToFrozenDictionary(x => x.Key, x => x.Value));
    }

    public EnumerableConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>>
      AsReadOnlyDictionary()
    {
      return Conversion(enumerableGenerator,
        enumerable => (IReadOnlyDictionary<TKey, TValue>)(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public EnumerableConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>>
      AsSortedDictionary()
    {
      return Conversion(enumerableGenerator,
        enumerable => new SortedDictionary<TKey, TValue>(
          enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public EnumerableConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>>
      AsConcurrentDictionary()
    {
      return Conversion(enumerableGenerator,
        enumerable => new ConcurrentDictionary<TKey, TValue>(enumerable));
    }

    public EnumerableConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>>
      AsSortedList()
    {
      return Conversion(enumerableGenerator,
        enumerable => new SortedList<TKey, TValue>(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }
  }

  extension<T1, T2>(InlineGenerator<IEnumerable<KeyValuePair<T1, T2>>> enumerableGenerator) where T1 : notnull
  {
    public InlineGenerator<ImmutableDictionary<T1, T2>> AsImmutableDictionary(
)
    {
      return Conversion(enumerableGenerator, ImmutableDictionary.ToImmutableDictionary);
    }

    public InlineGenerator<ImmutableSortedDictionary<T1, T2>> AsImmutableSortedDictionary(
  )
    {
      return Conversion(enumerableGenerator, ImmutableSortedDictionary.ToImmutableSortedDictionary);
    }
  }

  private static EnumerableConversion<T, U> Conversion<T, U>(
    InlineGenerator<IEnumerable<T>> enumerableGenerator,
    Func<IEnumerable<T>, U> conversion)
  {
    return new EnumerableConversion<T, U>(enumerableGenerator, conversion);
  }
}
