using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Math;

public static class AnyMathExtensions
{
  extension(BasicGenerator gen)
  {
    public int IntegerDivisibleBy(int quotient)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.IntegerDivisibleBy(quotient));
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.IntegerNotDivisibleBy(quotient));
    }

    public int IntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.IntegerWithExactDigitCount(digitsCount));
    }

    public long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.LongWithExactDigitCount(digitsCount));
    }

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.UnsignedIntWithExactDigitCount(digitsCount));
    }

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.UnsignedLongWithExactDigitCount(digitsCount));
    }
  }
}