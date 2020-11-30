using System;

namespace TddXt.AnyGenerators.CommonTypes
{
  public static class CircularList
  {
    private static readonly Random Random = new(DateTime.UtcNow.Millisecond);

    public static CircularList<T> StartingWithFirstOf<T>(params T[] items) 
      => new(0, items);

    public static CircularList<T> CreateStartingFromRandom<T>(params T[] items) 
      => new(Random.Next(0,items.Length - 1), items);
  }

  public class CircularList<T>
  {
    private readonly T[] _items;
    private int _startingIndex;

    public CircularList(int startingIndex, params T[] items)
    {
      _items = items;
      _startingIndex = startingIndex;
    }

    public T Next()
    {
      if(_startingIndex > _items.Length - 1)
      {
        _startingIndex = 0; 
      }
      var result = _items[_startingIndex];
      _startingIndex++;
      return result;
    }
  }
}
