using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Generators;

namespace TddEbook.TddToolkit.Generators
{
  public class InlineGenerators
  {
    public static InlineGenerator<IEnumerable<T>> EnumerableWith<T>(IEnumerable<T> included)
    {
      return new InclusiveEnumerableGenerator<T>(included);
    }

    public static InlineGenerator<IEnumerable<T>> Enumerable<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many);
    }

    public static InlineGenerator<IEnumerable<T>> Enumerable<T>(int length)
    {
      return new EnumerableGenerator<T>(length);
    }

    public static InlineGenerator<IEnumerable<T>> EnumerableWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded);
    }

    public static InlineGenerator<T[]> Array<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsArray();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<T[]> Array<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsArray();
    }

    public static InlineGenerator<T[]> ArrayWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded).AsArray();
    }

    public static InlineGenerator<T[]> ArrayWith<T>(T[] included)
    {
      return new InclusiveEnumerableGenerator<T>(included).AsArray();
    }

    public static InlineGenerator<List<T>> List<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsList();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<List<T>> List<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsList();
    }

    public static InlineGenerator<List<T>> ListWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded).AsList();
    }

    public static InlineGenerator<List<T>> ListWith<T>(T[] included)
    {
      return new InclusiveEnumerableGenerator<T>(included).AsList();
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>> SortedList<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsSortedList();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>> SortedList<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsSortedList();
    }

    public static ResultConversion<T, ISet<T>> Set<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<T, ISet<T>> Set<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSet();
    }

    public static ResultConversion<T, SortedSet<T>> SortedSet<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSortedSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<T, SortedSet<T>> SortedSet<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSortedSet();
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsReadOnlyDictionary();
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsReadOnlyDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> Dictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsDictionary();
    }


    public static ResultConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsConcurrentDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsConcurrentDictionary();
    }

    public static ResultConversion<T, ConcurrentStack<T>> ConcurrentStack<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentStack();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<T, ConcurrentStack<T>> ConcurrentStack<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentStack();
    }

    public static ResultConversion<T, ConcurrentQueue<T>> ConcurrentQueue<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentQueue();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<T, ConcurrentQueue<T>> ConcurrentQueue<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentQueue();
    }

    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> SortedDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsSortedDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> SortedDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsSortedDictionary();
    }

    public static ResultConversion<T, ConcurrentBag<T>> ConcurrentBag<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentBag();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static ResultConversion<T, ConcurrentBag<T>> ConcurrentBag<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentBag();
    }

    public static EnumeratorGenerator<T> Enumerator<T>()
    {
      return new EnumeratorGenerator<T>();
    }

    public static InlineGenerator<object> GetByNameAndType(string methodName, Type type)
    {
      var genericMethodProxyCalls = new GenericMethodProxyCalls();
      var inlineGenerator = genericMethodProxyCalls
        .ResultOfGenericVersionOfStaticMethod<InlineGenerators>(type, methodName);
      return ((InlineGenerator<object>)inlineGenerator);
    }

    public static InlineGenerator<object> GetByNameAndTypes(string methodName, Type type1, Type type2)
    {
      var genericMethodProxyCalls = new GenericMethodProxyCalls();
      var inlineGenerator = genericMethodProxyCalls
        .ResultOfGenericVersionOfStaticMethod<InlineGenerators>(type1, type2, methodName);
      return ((InlineGenerator<object>)inlineGenerator);
    }
  }
}