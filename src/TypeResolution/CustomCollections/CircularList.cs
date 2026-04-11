using System;
using System.Threading;
using Core.NullableReferenceTypesExtensions;

namespace TddXt.TypeResolution.CustomCollections;

public static class CircularList
{
  private static readonly ThreadLocal<Random> Random = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public static CircularList<T> StartingWithFirstOf<T>(params T[] items)
    => new(0, items);

  public static CircularList<T> CreateStartingFromRandom<T>(params T[] items)
    => new(Random.Value.OrThrow().Next(0, items.Length - 1), items);
}

public class CircularList<T>(int startingIndex, params T[] items)
{
  private readonly object _syncRoot = new();

  public T Next()
  {
    lock (_syncRoot)
    {
      if (startingIndex > items.Length - 1)
      {
        startingIndex = 0;
      }

      var result = items[startingIndex];
      startingIndex++;
      return result;
    }
  }
}
