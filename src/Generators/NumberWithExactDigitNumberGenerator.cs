using System;
using TddEbook.TddToolkit.Generators;

namespace Generators
{
  public class NumberWithExactDigitNumberGenerator<T>
  {
    private static readonly Random RandomGenerator = new Random(Guid.NewGuid().GetHashCode());
    private readonly NumericTraits<T> _intTraits;
    private readonly int _digitsCount;

    public NumberWithExactDigitNumberGenerator(NumericTraits<T> intTraits, int digitsCount)
    {
      _intTraits = intTraits;
      _digitsCount = digitsCount;
    }

    public T GenerateInstance(AllGenerator genAllGenerator)
    {
      return _intTraits.GenerateWithExactNumberOfDigits(_digitsCount, RandomGenerator);
    }
  }
}