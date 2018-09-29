using System;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Math
{
  public class NumberWithExactDigitNumberGenerator<T> : InlineGenerator<T>
  {
    private static readonly Random RandomGenerator = new Random(Guid.NewGuid().GetHashCode());
    private readonly NumericTraits<T> _intTraits;
    private readonly int _digitsCount;

    public NumberWithExactDigitNumberGenerator(NumericTraits<T> intTraits, int digitsCount)
    {
      _intTraits = intTraits;
      _digitsCount = digitsCount;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _intTraits.GenerateWithExactNumberOfDigits(_digitsCount, RandomGenerator);
    }
  }
}