using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Collections;

// ReSharper disable UnusedMember.Global
public static class AnyCollectionExtensions
{
  extension(BasicGenerator gen)
  {
    public IEnumerable<T> Enumerable<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Enumerable<T>());
    }

    public IEnumerable<T> Enumerable<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Enumerable<T>(length));
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.EnumerableWith(included));
    }

    public IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.EnumerableWithout(excluded));
    }

    public T[] Array<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Array<T>());
    }

    public T[] Array<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Array<T>(length));
    }

    public T[] ArrayWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ArrayWithout(excluded));
    }

    public T[] ArrayWith<T>(params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ArrayWith(included));
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ArrayWithout(excluded.ToArray()));
    }

    public T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ArrayWith(included.ToArray()));
    }

    public List<T> List<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.List<T>());
    }

    public List<T> List<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.List<T>(length));
    }

    public List<T> ListWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(excluded));
    }

    public List<T> ListWith<T>(params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWith(included));
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(excluded.ToArray()));
    }

    public List<T> ListWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWith(included.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.List<T>());
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.List<T>(length));
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWith(items.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWith(items));
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(items.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ListWithout(items));
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedList<TKey, TValue>());
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedList<TKey, TValue>(length));
    }

    public ISet<T> Set<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Set<T>(length));
    }

    public ISet<T> Set<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Set<T>());
    }

    // ReSharper disable once UnusedMember.Global
    public ISet<T> SortedSet<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>(length));
    }

    public ISet<T> SortedSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>());
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Dictionary<TKey, TValue>(length));
    }

    public Dictionary<TKey, TValue> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Dictionary<TKey, TValue>());
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ReadOnlyDictionary<TKey, TValue>(length));
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ReadOnlyDictionary<TKey, TValue>());
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedDictionary<TKey, TValue>(length));
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedDictionary<TKey, TValue>());
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentDictionary<TKey, TValue>(length));
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentDictionary<TKey, TValue>());
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentStack<T>());
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentStack<T>(length));
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentQueue<T>());
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentQueue<T>(length));
    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentBag<T>());
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ConcurrentBag<T>(length));
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>(length));
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.SortedSet<T>());
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.Enumerator<T>());
    }

    public ImmutableArray<T> ImmutableArray<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableArray<T>());
    }
    public ImmutableList<T> ImmutableList<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableList<T>());
    }
    public ImmutableHashSet<T> ImmutableHashSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableHashSet<T>());
    }
    public ImmutableSortedSet<T> ImmutableSortedSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableSortedSet<T>());
    }

    public ImmutableDictionary<TKey, TValue> ImmutableDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableDictionary<TKey, TValue>());
    }

    public ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableSortedDictionary<TKey, TValue>());
    }

    public ImmutableQueue<T> ImmutableQueue<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableQueue<T>());
    }

    public ImmutableStack<T> ImmutableStack<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.ImmutableStack<T>());
    }

    public FrozenSet<T> FrozenSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.FrozenSet<T>());
    }

    public FrozenDictionary<TKey, TValue> FrozenDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.FrozenDictionary<TKey, TValue>());
    }
  }
}
