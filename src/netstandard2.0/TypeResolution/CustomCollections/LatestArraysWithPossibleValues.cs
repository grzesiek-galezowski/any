using System;
using System.Collections.Generic;
using System.Linq;

namespace TddXt.TypeResolution.CustomCollections;

public class LatestArraysWithPossibleValues<T>
{
  private const int CacheSize = 20;
  private readonly List<ArrayWithIndex<T>> _arrays = [];
  private readonly Random _random = new();

  public bool Contain(IEnumerable<T> possibleValues)
  {
    return _arrays.Any(array => array.IsEquivalentTo(possibleValues.ToArray()));
  }

  public void Add(IEnumerable<T> possibleValues)
  {
    if (_arrays.Count > CacheSize)
    {
      _arrays.RemoveAt(0);
    }
    _arrays.Add(new ArrayWithIndex<T>(
      possibleValues.ToArray(),
      _random.Next(0, possibleValues.Count())));
  }

  public T PickNextElementFrom(T[] possibleValues)
  {
    return _arrays.Single(a => a.IsEquivalentTo(possibleValues)).GetNextElement();
  }
}
