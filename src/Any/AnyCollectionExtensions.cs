using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AnyCore
{
    public static class AnyCollectionExtensions
    {
    public static IEnumerable<T> Enumerable<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Enumerable<T>();
    }

    public static IEnumerable<T> Enumerable<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.Enumerable<T>(length);
    }

    public static IEnumerable<T> EnumerableWithout<T>(this MyGenerator gen, params T[] excluded)
    {
      return gen.AllGenerator.EnumerableWithout(excluded);
    }

    public static T[] Array<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Array<T>();
    }

    public static T[] Array<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.Array<T>(length);
    }

    public static T[] ArrayWithout<T>(this MyGenerator gen, params T[] excluded)
    {
      return gen.AllGenerator.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(this MyGenerator gen, params T[] included)
    {
      return gen.AllGenerator.ArrayWith(included);
    }

    public static T[] ArrayWithout<T>(this MyGenerator gen, IEnumerable<T> excluded)
    {
      return gen.AllGenerator.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(this MyGenerator gen, IEnumerable<T> included)
    {
      return gen.AllGenerator.ArrayWith(included);
    }

    public static List<T> List<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.List<T>();
    }

    public static List<T> List<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.List<T>(length);
    }

    public static List<T> ListWithout<T>(this MyGenerator gen, params T[] excluded)
    {
      return gen.AllGenerator.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(this MyGenerator gen, params T[] included)
    {
      return gen.AllGenerator.ListWith(included);
    }

    public static List<T> ListWithout<T>(this MyGenerator gen, IEnumerable<T> excluded)
    {
      return gen.AllGenerator.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(this MyGenerator gen, IEnumerable<T> included)
    {
      return gen.AllGenerator.ListWith(included);
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ReadOnlyList<T>();
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ReadOnlyList<T>(length);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(this MyGenerator gen, IEnumerable<T> items)
    {
      return gen.AllGenerator.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(this MyGenerator gen, params T[] items)
    {
      return gen.AllGenerator.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(this MyGenerator gen, IEnumerable<T> items)
    {
      return gen.AllGenerator.ReadOnlyListWithout(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(this MyGenerator gen, params T[] items)
    {
      return gen.AllGenerator.ReadOnlyListWithout(items);
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(this MyGenerator gen)
    {
      return gen.AllGenerator.SortedList<TKey, TValue>();
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.SortedList<TKey, TValue>(length);
    }


    public static ISet<T> Set<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.Set<T>(length);
    }

    public static ISet<T> Set<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Set<T>();
    }

    public static ISet<T> SortedSet<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.SortedSet<T>(length);
    }

    public static ISet<T> SortedSet<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.SortedSet<T>();
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.Dictionary<TKey, TValue>(length);
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(this MyGenerator gen, IEnumerable<T> keys)
    {
      return gen.AllGenerator.DictionaryWithKeys<T, U>(keys);
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(this MyGenerator gen)
    {
      return gen.AllGenerator.Dictionary<TKey, TValue>();
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ReadOnlyDictionary<TKey, TValue>(length);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(this MyGenerator gen, IEnumerable<TKey> keys)
    {
      return gen.AllGenerator.ReadOnlyDictionaryWithKeys<TKey, TValue>(keys);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(this MyGenerator gen)
    {
      return gen.AllGenerator.ReadOnlyDictionary<TKey, TValue>();
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.SortedDictionary<TKey, TValue>(length);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(this MyGenerator gen)
    {
      return gen.AllGenerator.SortedDictionary<TKey, TValue>();
    }
    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ConcurrentDictionary<TKey, TValue>(length);
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(this MyGenerator gen)
    {
      return gen.AllGenerator.ConcurrentDictionary<TKey, TValue>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ConcurrentStack<T>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ConcurrentStack<T>(length);
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ConcurrentQueue<T>();
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ConcurrentQueue<T>(length);
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ConcurrentBag<T>();
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.ConcurrentBag<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(this MyGenerator gen, int length)
    {
      return gen.AllGenerator.EnumerableSortedDescending<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.EnumerableSortedDescending<T>();
    }

    public static IEnumerator<T> Enumerator<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Enumerator<T>();
    }

    public static object List(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.List(type);
    }

    public static object Set(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.Set(type);
    }

    public static object SortedList(this MyGenerator gen, Type keyType, Type valueType)
    {
      return gen.AllGenerator.SortedList(keyType, valueType);
    }

    public static object SortedSet(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.SortedSet(type);
    }

    public static object ConcurrentDictionary(this MyGenerator gen, Type keyType, Type valueType)
    {
      return gen.AllGenerator.ConcurrentDictionary(keyType, valueType);
    }

    public static object Array(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.Array(type);
    }

    private static ICollection<T> AddManyTo<T>(this MyGenerator gen, ICollection<T> collection, int many)
    {
      return gen.AllGenerator.AddManyTo(collection, many);
    }

    public static object Enumerator(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.Enumerator(type);
    }

    public static object ConcurrentStack(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.ConcurrentStack(type);
    }

    public static object ConcurrentQueue(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.ConcurrentQueue(type);
    }

    public static object ConcurrentBag(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.ConcurrentBag(type);
    }

  }
}
