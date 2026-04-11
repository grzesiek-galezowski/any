using System.Collections.Generic;
using System.Linq;

namespace TddXt.TypeResolution.CustomCollections;

public class ArrayWithIndex<T>(T[] values, int initialIndex)
{
  private int Index { get; set; } = initialIndex;

  private IEnumerable<T> Values
  {
    get { return values; }
  }

  public bool IsEquivalentTo(IEnumerable<T> array)
  {
    return Values.SequenceEqual(array);
  }

  public T GetNextElement()
  {
    if (Index == values.Length)
    {
      Index = 0;
    }

    var result = values[Index];
    Index++;
    return result;
  }
}
