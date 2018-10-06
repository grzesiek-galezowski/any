using System.Collections.Generic;
using System.Linq;

namespace TddXt.TypeResolution.CustomCollections
{
  public class ArrayWithIndex<T>
  {
    private readonly T[] _values;

    public ArrayWithIndex(T[] values, int initialIndex)
    {
      _values = values;
      Index = initialIndex;
    }

    private int Index { get; set; }

    private IEnumerable<T> Values
    {
      get { return _values; }
    }

    public bool IsEquivalentTo(IEnumerable<T> array)
    {
      return Values.SequenceEqual(array);
    }

    public T GetNextElement()
    {
      if (Index == _values.Length)
      {
        Index = 0;
      }
      var result = _values[Index];
      Index++;
      return result;
    }
  }
}