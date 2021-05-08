using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;
using TddXt.AnyGenerators.Root.ImplementationDetails;

namespace TddXt.AnyRoot.Collections
{
  // ReSharper disable UnusedMember.Global

  public static class AnyCollectionExtensions
  {
    public static IEnumerable<T> Enumerable<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Enumerable<T>());
    }

    public static IEnumerable<T> Enumerable<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.Enumerable<T>(length));
    }

    public static IEnumerable<T> EnumerableWith<T>(this BasicGenerator gen, IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.EnumerableWith(included));
    }

    public static IEnumerable<T> EnumerableWithout<T>(this BasicGenerator gen, params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.EnumerableWithout(excluded));
    }

    public static T[] Array<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Array<T>());
    }

    public static T[] Array<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.Array<T>(length));
    }

    public static T[] ArrayWithout<T>(this BasicGenerator gen, params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWithout(excluded));
    }

    public static T[] ArrayWith<T>(this BasicGenerator gen, params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWith(included));
    }

    public static T[] ArrayWithout<T>(this BasicGenerator gen, IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWithout(excluded.ToArray()));
    }

    public static T[] ArrayWith<T>(this BasicGenerator gen, IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWith(included.ToArray()));
    }

    public static List<T> List<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.List<T>());
    }

    public static List<T> List<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.List<T>(length));
    }

    public static List<T> ListWithout<T>(this BasicGenerator gen, params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(excluded));
    }

    public static List<T> ListWith<T>(this BasicGenerator gen, params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(included));
    }

    public static List<T> ListWithout<T>(this BasicGenerator gen, IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(excluded.ToArray()));
    }

    public static List<T> ListWith<T>(this BasicGenerator gen, IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(included.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.List<T>());
    }

    public static IReadOnlyList<T> ReadOnlyList<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.List<T>(length));
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(this BasicGenerator gen, IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(items.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyListWith<T>(this BasicGenerator gen, params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(items));
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(this BasicGenerator gen, IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(items.ToArray()));
    }

    public static IReadOnlyList<T> ReadOnlyListWithout<T>(this BasicGenerator gen, params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(items));
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.SortedList<TKey, TValue>());
    }

    public static SortedList<TKey, TValue> SortedList<TKey, TValue>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedList<TKey, TValue>(length));
    }

    public static ISet<T> Set<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.Set<T>(length));
    }

    public static ISet<T> Set<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Set<T>());
    }

    // ReSharper disable once UnusedMember.Global
    public static ISet<T> SortedSet<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>(length));
    }

    public static ISet<T> SortedSet<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>());
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.Dictionary<TKey, TValue>(length));
    }

    public static Dictionary<T, U> DictionaryWithKeys<T, U>(this BasicGenerator gen, IEnumerable<T> keys)
    {
      return gen.InstanceOf(InlineGenerators.DictionaryWithKeys<T, U>(keys));
    }

    public static Dictionary<TKey, TValue> Dictionary<TKey, TValue>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Dictionary<TKey, TValue>());
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.ReadOnlyDictionary<TKey, TValue>(length));
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(this BasicGenerator gen, IEnumerable<TKey> keys)
    {
      return gen.InstanceOf(InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public static IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ReadOnlyDictionary<TKey, TValue>());
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedDictionary<TKey, TValue>(length));
    }

    public static SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.SortedDictionary<TKey, TValue>());
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentDictionary<TKey, TValue>(length));
    }

    public static ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentDictionary<TKey, TValue>());
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentStack<T>());
    }

    public static ConcurrentStack<T> ConcurrentStack<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentStack<T>(length));
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentQueue<T>());
    }

    public static ConcurrentQueue<T> ConcurrentQueue<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentQueue<T>(length));
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentBag<T>());
    }

    public static ConcurrentBag<T> ConcurrentBag<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentBag<T>(length));
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>(length));
    }

    public static IEnumerable<T> EnumerableSortedDescending<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>());
    }

    public static IEnumerator<T> Enumerator<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Enumerator<T>());
    }

    public static ImmutableArray<T> ImmutableArray<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableArray<T>());
    }
    public static ImmutableList<T> ImmutableList<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableList<T>());
    }
    public static ImmutableHashSet<T> ImmutableHashSet<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableHashSet<T>());
    }
    public static ImmutableSortedSet<T> ImmutableSortedSet<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableSortedSet<T>());
    }

    public static ImmutableDictionary<T1, T2> ImmutableDictionary<T1, T2>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableDictionary<T1, T2>());
    }

    public static ImmutableSortedDictionary<T1, T2> ImmutableSortedDictionary<T1, T2>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableSortedDictionary<T1, T2>());
    }

    public static ImmutableQueue<T> ImmutableQueue<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableQueue<T>());
    }
    public static ImmutableStack<T> ImmutableStack<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.ImmutableStack<T>());
    }
  }
}
