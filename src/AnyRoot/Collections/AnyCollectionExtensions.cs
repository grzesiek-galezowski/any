using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Collections;

// ReSharper disable UnusedMember.Global
public static class AnyCollectionExtensions
{
  extension(BasicGenerator gen)
  {
    public IEnumerable<T> Enumerable<T>()
    {
      return gen.InstanceOf(InlineGenerators.Enumerable<T>());
    }

    public IEnumerable<T> Enumerable<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.Enumerable<T>(length));
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.EnumerableWith(included));
    }

    public IEnumerable<T> EnumerableWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.EnumerableWithout(excluded));
    }

    public T[] Array<T>()
    {
      return gen.InstanceOf(InlineGenerators.Array<T>());
    }

    public T[] Array<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.Array<T>(length));
    }

    public T[] ArrayWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWithout(excluded));
    }

    public T[] ArrayWith<T>(params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWith(included));
    }

    public T[] ArrayWithout<T>(IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWithout(excluded.ToArray()));
    }

    public T[] ArrayWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.ArrayWith(included.ToArray()));
    }

    public List<T> List<T>()
    {
      return gen.InstanceOf(InlineGenerators.List<T>());
    }

    public List<T> List<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.List<T>(length));
    }

    public List<T> ListWithout<T>(params T[] excluded)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(excluded));
    }

    public List<T> ListWith<T>(params T[] included)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(included));
    }

    public List<T> ListWithout<T>(IEnumerable<T> excluded)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(excluded.ToArray()));
    }

    public List<T> ListWith<T>(IEnumerable<T> included)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(included.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return gen.InstanceOf(InlineGenerators.List<T>());
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.List<T>(length));
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(items.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.ListWith(items));
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(items.ToArray()));
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return gen.InstanceOf(InlineGenerators.ListWithout(items));
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.SortedList<TKey, TValue>());
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.SortedList<TKey, TValue>(length));
    }

    public ISet<T> Set<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.Set<T>(length));
    }

    public ISet<T> Set<T>()
    {
      return gen.InstanceOf(InlineGenerators.Set<T>());
    }

    // ReSharper disable once UnusedMember.Global
    public ISet<T> SortedSet<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>(length));
    }

    public ISet<T> SortedSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>());
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.Dictionary<TKey, TValue>(length));
    }

    public Dictionary<TKey, TValue> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.Dictionary<TKey, TValue>());
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ReadOnlyDictionary<TKey, TValue>(length));
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys));
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ReadOnlyDictionary<TKey, TValue>());
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.SortedDictionary<TKey, TValue>(length));
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.SortedDictionary<TKey, TValue>());
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length) where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentDictionary<TKey, TValue>(length));
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentDictionary<TKey, TValue>());
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentStack<T>());
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentStack<T>(length));
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentQueue<T>());
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentQueue<T>(length));
    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentBag<T>());
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.ConcurrentBag<T>(length));
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>(length));
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return gen.InstanceOf(InlineGenerators.SortedSet<T>());
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return gen.InstanceOf(InlineGenerators.Enumerator<T>());
    }

    public ImmutableArray<T> ImmutableArray<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableArray<T>());
    }
    public ImmutableList<T> ImmutableList<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableList<T>());
    }
    public ImmutableHashSet<T> ImmutableHashSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableHashSet<T>());
    }
    public ImmutableSortedSet<T> ImmutableSortedSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableSortedSet<T>());
    }

    public ImmutableDictionary<TKey, TValue> ImmutableDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ImmutableDictionary<TKey, TValue>());
    }

    public ImmutableSortedDictionary<TKey, TValue> ImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.ImmutableSortedDictionary<TKey, TValue>());
    }

    public ImmutableQueue<T> ImmutableQueue<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableQueue<T>());
    }

    public ImmutableStack<T> ImmutableStack<T>()
    {
      return gen.InstanceOf(InlineGenerators.ImmutableStack<T>());
    }

    public FrozenSet<T> FrozenSet<T>()
    {
      return gen.InstanceOf(InlineGenerators.FrozenSet<T>());
    }

    public FrozenDictionary<TKey, TValue> FrozenDictionary<TKey, TValue>() where TKey : notnull
    {
      return gen.InstanceOf(InlineGenerators.FrozenDictionary<TKey, TValue>());
    }
  }
}
