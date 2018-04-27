using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {

    public static IEnumerable<T> Enumerable<T>()
    {
      return gen.Enumerable<T>();
    }

    public static IEnumerable<T> Enumerable<T>(int length)
    {
      return gen.Enumerable<T>(length);
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return gen.EnumerableWithout(excluded);
    }

    public static T[] Array<T>()
    {
      return gen.Array<T>();
    }

    public static T[] Array<T>(int length)
    {
      return gen.Array<T>(length);
    }

    public static T[] ArrayWithout<T>(params T[] excluded)
    {
      return gen.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(params T[] included)
    {
      return gen.ArrayWith(included);
    }

    public static T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return gen.ArrayWithout(excluded);
    }

    public static T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return gen.ArrayWith(included);
    }

    public static List<T> List<T>()
    {
      return gen.List<T>();
    }

    public static List<T> List<T>(int length)
    {
      return gen.List<T>(length);
    }

    public static List<T> ListWithout<T>(params T[] excluded)
    {
      return gen.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(params T[] included)
    {
      return gen.ListWith(included);
    }

    public static List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return gen.ListWithout(excluded);
    }

    public static List<T> ListWith<T>(IEnumerable<T> included)
    {
      return gen.ListWith(included);
    }

    public static IReadOnlyList<T> ReadOnlyList<T>()
    {
      return gen.ReadOnlyList<T>();
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return gen.ReadOnlyList<T>(length);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return gen.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return gen.ReadOnlyListWith(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return gen.ReadOnlyListWithout(items);
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return gen.ReadOnlyListWithout(items);
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return gen.SortedList<TKey, TValue>();
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      return gen.SortedList<TKey, TValue>(length);
    }


    public static ISet<T> Set<T>(int length)
    {
      return gen.Set<T>(length);
    }

    public static ISet<T> Set<T>()
    {
      return gen.Set<T>();
    }

    public static ISet<T> SortedSet<T>(int length)
    {
      return gen.SortedSet<T>(length);
    }

    public static ISet<T> SortedSet<T>()
    {
      return gen.SortedSet<T>();
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return gen.Dictionary<TKey, TValue>(length);
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(IEnumerable<T> keys)
    {
      return gen.DictionaryWithKeys<T, U>(keys);
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return gen.Dictionary<TKey, TValue>();
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return gen.ReadOnlyDictionary<TKey, TValue>(length);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return gen.ReadOnlyDictionaryWithKeys<TKey, TValue>(keys);
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return gen.ReadOnlyDictionary<TKey, TValue>();
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      return gen.SortedDictionary<TKey, TValue>(length);
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return gen.SortedDictionary<TKey, TValue>();
    }
    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return gen.ConcurrentDictionary<TKey, TValue>(length);
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return gen.ConcurrentDictionary<TKey, TValue>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>()
    {
      return gen.ConcurrentStack<T>();
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return gen.ConcurrentStack<T>(length);
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return gen.ConcurrentQueue<T>();
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return gen.ConcurrentQueue<T>(length);
    }

    public static ConcurrentBag<T> ConcurrentBag<T>()
    {
      return gen.ConcurrentBag<T>();
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return gen.ConcurrentBag<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return gen.EnumerableSortedDescending<T>(length);
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return gen.EnumerableSortedDescending<T>();
    }

    public static IEnumerator<T> Enumerator<T>()
    {
      return gen.Enumerator<T>();
    }

    public static object List(Type type)
    {
      return gen.List(type);
    }

    public static object Set(Type type)
    {
      return gen.Set(type);
    }

    public static object SortedList(Type keyType, Type valueType)
    {
      return gen.SortedList(keyType, valueType);
    }

    public static object SortedSet(Type type)
    {
      return gen.SortedSet(type);
    }

    public static object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return gen.ConcurrentDictionary(keyType, valueType);
    }

    public static object Array(Type type)
    {
      return gen.Array(type);
    }

    private static ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      return gen.AddManyTo(collection, many);
    }

    public static object Enumerator(Type type)
    {
      return gen.Enumerator(type);
    }

    public static object ConcurrentStack(Type type)
    {
      return gen.ConcurrentStack(type);
    }

    public static object ConcurrentQueue(Type type)
    {
      return gen.ConcurrentQueue(type);
    }

    public static object ConcurrentBag(Type type)
    {
      return gen.ConcurrentBag(type);
    }


  }
}
