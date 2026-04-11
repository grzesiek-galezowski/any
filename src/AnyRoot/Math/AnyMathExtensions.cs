using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Math;

public static class AnyMathExtensions
{
  extension(BasicGenerator gen)
  {
    public int IntegerDivisibleBy(int quotient)
    {
      return gen.InstanceOf(InlineGenerators.IntegerDivisibleBy(quotient));
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return gen.InstanceOf(InlineGenerators.IntegerNotDivisibleBy(quotient));
    }

    public int IntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.IntegerWithExactDigitCount(digitsCount));
    }

    public long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.LongWithExactDigitCount(digitsCount));
    }

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedIntWithExactDigitCount(digitsCount));
    }

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedLongWithExactDigitCount(digitsCount));
    }
  }
}