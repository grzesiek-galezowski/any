using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace TddXt.AnyRoot.Collections;

// ReSharper disable UnusedMember.Global
public static class AnyCollectionExtensions
{
  extension(Any)
  {
    public static IEnumerable<T> Enumerable<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Enumerable<T>());
    }

    public static IEnumerable<T> Enumerable<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Enumerable<T>(length));
    }

    public static IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.EnumerableWith(included));
    }

    public static IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.EnumerableWithout(excluded));
    }

    public static T[] Array<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Array<T>());
    }

    public static T[] Array<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Array<T>(length));
    }

    public static T[] ArrayWithout<T>(params T[] excluded)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ArrayWithout(excluded));
    }

    public static T[] ArrayWith<T>(params T[] included)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ArrayWith(included));
    }

    public static T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ArrayWithout(excluded.ToArray()));
    }

    public static T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ArrayWith(included.ToArray()));
    }

    public static List<T> List<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.List<T>());
    }

    public static List<T> List<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.List<T>(length));
    }

    public static List<T> ListWithout<T>(params T[] excluded)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(excluded));
    }

    public static List<T> ListWith<T>(params T[] included)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWith(included));
    }

    public static List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(excluded.ToArray()));
    }

    public static List<T> ListWith<T>(IEnumerable<T> included)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWith(included.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyList<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.List<T>());
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.List<T>(length));
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWith(items.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWith(items));
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(items.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(items));
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedList<TKey, TValue>());
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(int length) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedList<TKey, TValue>(length));
    }

    public static ISet<T> Set<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Set<T>(length));
    }

    public static ISet<T> Set<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Set<T>());
    }

    // ReSharper disable once UnusedMember.Global
    public static ISet<T> SortedSet<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>(length));
    }

    public static ISet<T> SortedSet<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>());
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Dictionary<TKey, TValue>(length));
    }

    public static Dictionary<TKey, TValue> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Dictionary<TKey, TValue>());
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ReadOnlyDictionary<TKey, TValue>(length));
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ReadOnlyDictionary<TKey, TValue>());
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedDictionary<TKey, TValue>(length));
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedDictionary<TKey, TValue>());
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentDictionary<TKey, TValue>(length));
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentDictionary<TKey, TValue>());
    }

    public static ConcurrentStack<T> ConcurrentStack<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentStack<T>());
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentStack<T>(length));
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentQueue<T>());
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentQueue<T>(length));
    }

    public static ConcurrentBag<T> ConcurrentBag<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentBag<T>());
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentBag<T>(length));
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>(length));
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>());
    }

    public static IEnumerator<T> Enumerator<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Enumerator<T>());
    }

    public static ImmutableArray<T> ImmutableArray<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableArray<T>());
    }
    public static ImmutableList<T> ImmutableList<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableList<T>());
    }
    public static ImmutableHashSet<T> ImmutableHashSet<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableHashSet<T>());
    }
    public static ImmutableSortedSet<T> ImmutableSortedSet<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableSortedSet<T>());
    }

    public static ImmutableDictionary<TKey, TValue> ImmutableDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableDictionary<TKey, TValue>());
    }

    public static ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableSortedDictionary<TKey, TValue>());
    }

    public static ImmutableQueue<T> ImmutableQueue<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableQueue<T>());
    }

    public static ImmutableStack<T> ImmutableStack<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ImmutableStack<T>());
    }

    public static FrozenSet<T> FrozenSet<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.FrozenSet<T>());
    }

    public static FrozenDictionary<TKey, TValue> FrozenDictionary<TKey, TValue>() where TKey : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.FrozenDictionary<TKey, TValue>());
    }
  }
}
