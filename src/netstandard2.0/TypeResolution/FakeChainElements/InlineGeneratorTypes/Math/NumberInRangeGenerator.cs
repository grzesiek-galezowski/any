using System;
using System.Collections.Generic;
using System.Numerics;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Math;

public class NumberInRangeGenerator<T> : InlineGenerator<T>
{
  private readonly BigInteger _min;
  private readonly BigInteger _max;
  private static readonly Dictionary<(BigInteger, BigInteger), BigInteger> Values = new();
  private static readonly object SyncRoot = new object();
  private readonly (BigInteger _min, BigInteger _max) _key;
  private readonly Func<BigInteger, T> _cast;


  public NumberInRangeGenerator(BigInteger min, BigInteger max, Func<BigInteger, T> cast)
  {
    _min = min;
    _max = max;
    _cast = cast;
    _key = (_min, _max);
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    lock (SyncRoot)
    {
      if (!Values.ContainsKey(_key))
      {
        Values[_key] = _min;
        return _cast(_min);
      }
      else
      {
        Values[_key]++;
        if (Values[_key] > _max)
        {
          Values[_key] = _min;
        }
        return _cast(Values[_key]);
      }
    }
  }
}
