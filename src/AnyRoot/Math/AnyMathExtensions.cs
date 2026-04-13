namespace TddXt.AnyRoot.Math;

public static class AnyMathExtensions
{
  extension(Any)
  {
    public static int IntegerDivisibleBy(int quotient)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.IntegerDivisibleBy(quotient));
    }

    public static int IntegerNotDivisibleBy(int quotient)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.IntegerNotDivisibleBy(quotient));
    }

    public static int IntegerWithExactDigitsCount(int digitsCount)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.IntegerWithExactDigitCount(digitsCount));
    }

    public static long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.LongWithExactDigitCount(digitsCount));
    }

    public static uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UnsignedIntWithExactDigitCount(digitsCount));
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UnsignedLongWithExactDigitCount(digitsCount));
    }
  }
}
