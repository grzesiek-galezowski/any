using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public static class InlineCollectionGeneratorExtensions
  {
    //todo these methods can be further generified. Leaving for now to see what syntax is needed

    public static EnumerableConversion<T, T[]> AsArray<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, Enumerable.ToArray);
    }

    public static EnumerableConversion<T, List<T>> AsList<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, Enumerable.ToList);
    }

    public static EnumerableConversion<T, IReadOnlyList<T>> AsReadOnlyList<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      Func<IEnumerable<T>, IReadOnlyList<T>> x = Enumerable.ToList;
      return Conversion(enumerableGenerator, x);
    }

    public static InlineGenerator<HashSet<T>> AsHashSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new HashSet<T>(enumerable));
    }

    public static EnumerableConversion<T, ISet<T>> AsSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => (ISet<T>)new HashSet<T>(enumerable));
    }

    public static EnumerableConversion<T, SortedSet<T>> AsSortedSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new SortedSet<T>(enumerable));
    }

    public static EnumerableConversion<T, ConcurrentStack<T>> AsConcurrentStack<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentStack<T>(enumerable));
    }

    public static EnumerableConversion<T, ConcurrentQueue<T>> AsConcurrentQueue<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentQueue<T>(enumerable));
    }

    public static EnumerableConversion<T, ConcurrentBag<T>> AsConcurrentBag<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentBag<T>(enumerable));
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> 
      AsDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => enumerable.ToDictionary(x => x.Key, x => x.Value));
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>>
      AsReadOnlyDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator,
        enumerable => (IReadOnlyDictionary<TKey, TValue>)(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> 
      AsSortedDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => new SortedDictionary<TKey, TValue>(
          enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> 
      AsConcurrentDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => new ConcurrentDictionary<TKey, TValue>(enumerable));
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>>
      AsSortedList<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator,
        enumerable => new SortedList<TKey, TValue>(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    private static EnumerableConversion<T, U> Conversion<T, U>(
      InlineGenerator<IEnumerable<T>> enumerableGenerator,
      Func<IEnumerable<T>, U> conversion)
    {
      return new EnumerableConversion<T, U>(enumerableGenerator, conversion);
    }
  }
}