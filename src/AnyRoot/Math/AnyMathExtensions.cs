using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Math
{
  public static class AnyMathExtensions
  {
    public static int IntegerDivisibleBy(this BasicGenerator gen, int quotient)
    {
      return gen.InstanceOf(InlineGenerators.IntegerDivisibleBy(quotient));
    }

    public static int IntegerNotDivisibleBy(this BasicGenerator gen, int quotient)
    {
      return gen.InstanceOf(InlineGenerators.IntegerNotDivisibleBy(quotient));
    }

    public static int IntegerWithExactDigitsCount(this BasicGenerator gen, int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.IntegerWithExactDigitCount(digitsCount));
    }

    public static long LongIntegerWithExactDigitsCount(this BasicGenerator gen, int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.LongWithExactDigitCount(digitsCount));
    }

    public static uint UnsignedIntegerWithExactDigitsCount(this BasicGenerator gen, int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedIntWithExactDigitCount(digitsCount));
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(this BasicGenerator gen, int digitsCount)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedLongWithExactDigitCount(digitsCount));
    }
  }
}