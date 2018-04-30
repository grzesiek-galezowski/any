using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TddEbook.TddToolkit.Generators;

namespace Generators
{
  public static class InlineGeneratorExtensions
  {
    //todo these methods can be further generified. Leaving for now to see what syntax is needed

    public static ResultConversion<T, T[]> AsArray<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, System.Linq.Enumerable.ToArray);
    }

    public static ResultConversion<T, List<T>> AsList<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, System.Linq.Enumerable.ToList);
    }

    public static ResultConversion<T, IReadOnlyList<T>> AsReadOnlyList<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      Func<IEnumerable<T>, IReadOnlyList<T>> x = Enumerable.ToList;
      return Conversion(enumerableGenerator, x);
    }

    public static ResultConversion<T, HashSet<T>> AsHashSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new HashSet<T>(enumerable));
    }

    public static ResultConversion<T, ISet<T>> AsSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => (ISet<T>)new HashSet<T>(enumerable));
    }

    public static ResultConversion<T, SortedSet<T>> AsSortedSet<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new SortedSet<T>(enumerable));
    }

    public static ResultConversion<T, ConcurrentStack<T>> AsConcurrentStack<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentStack<T>(enumerable));
    }

    public static ResultConversion<T, ConcurrentQueue<T>> AsConcurrentQueue<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentQueue<T>(enumerable));
    }

    public static ResultConversion<T, ConcurrentBag<T>> AsConcurrentBag<T>(this InlineGenerator<IEnumerable<T>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, enumerable => new ConcurrentBag<T>(enumerable));
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> 
      AsDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => enumerable.ToDictionary(x => x.Key, x => x.Value));
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>>
      AsReadOnlyDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator,
        enumerable => (IReadOnlyDictionary<TKey, TValue>)(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> 
      AsSortedDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => new SortedDictionary<TKey, TValue>(
          enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> 
      AsConcurrentDictionary<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator, 
        enumerable => new ConcurrentDictionary<TKey, TValue>(enumerable));
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>>
      AsSortedList<TKey, TValue>(this InlineGenerator<IEnumerable<KeyValuePair<TKey, TValue>>> enumerableGenerator)
    {
      return Conversion(enumerableGenerator,
        enumerable => new SortedList<TKey, TValue>(enumerable.ToDictionary(x => x.Key, x => x.Value)));
    }


    private static ResultConversion<T, U>  Conversion<T, U>(
      InlineGenerator<IEnumerable<T>> enumerableGenerator,
      Func<IEnumerable<T>, U> conversion)
    {
      return new ResultConversion<T, U>(enumerableGenerator, conversion);
    }
  }
}