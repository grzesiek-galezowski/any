using System;
using System.Threading;

namespace TddXt.TypeResolution.CustomCollections;

public static class CircularList
{
  private static readonly ThreadLocal<Random> Random = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public static CircularList<T> StartingWithFirstOf<T>(params T[] items)
    => new(0, items);

  public static CircularList<T> CreateStartingFromRandom<T>(params T[] items)
    => new(Random.Value.Next(0, items.Length - 1), items);
}

public class CircularList<T>
{
  private readonly T[] _items;
  private int _startingIndex;
  private readonly object _syncRoot = new();

  public CircularList(int startingIndex, params T[] items)
  {
    _items = items;
    _startingIndex = startingIndex;
  }

  public T Next()
  {
    lock (_syncRoot)
    {
      if (_startingIndex > _items.Length - 1)
      {
        _startingIndex = 0;
      }

      var result = _items[_startingIndex];
      _startingIndex++;
      return result;
    }
  }
}
