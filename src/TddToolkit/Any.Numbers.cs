namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    public static int Integer()
    {
      return gen.Integer();
    }

    public static double Double()
    {
      return gen.Double();
    }

    public static double DoubleOtherThan(params double[] others)
    {
      return gen.DoubleOtherThan(others);
    }

    public static long LongInteger()
    {
      return gen.LongInteger();
    }

    public static long LongIntegerOtherThan(params long[] others)
    {
      return gen.LongIntegerOtherThan(others);
    }

    public static ulong UnsignedLongInteger()
    {
      return gen.UnsignedLongInteger();
    }

    public static ulong UnsignedLongIntegerOtherThan(params ulong[] others)
    {
      return gen.UnsignedLongIntegerOtherThan(others);
    }

    public static int IntegerOtherThan(params int[] others)
    {
      return gen.IntegerOtherThan(others);
    }

    public static byte Byte()
    {
      return gen.Byte();
    }

    public static byte ByteOtherThan(params byte[] others)
    {
      return gen.ByteOtherThan(others);
    }

    public static decimal Decimal()
    {
      return gen.Decimal();
    }

    public static decimal DecimalOtherThan(params decimal[] others)
    {
      return gen.DecimalOtherThan(others);
    }

    public static uint UnsignedInt()
    {
      return gen.UnsignedInt();
    }

    public static uint UnsignedIntOtherThan(params uint[] others)
    {
      return gen.UnsignedIntOtherThan(others);
    }

    public static ushort UnsignedShort()
    {
      return gen.UnsignedShort();
    }

    public static ushort UnsignedShortOtherThan(params ushort[] others)
    {
      return gen.UnsignedShortOtherThan(others);
    }

    public static short ShortInteger()
    {
      return gen.ShortInteger();
    }

    public static short ShortIntegerOtherThan(params short[] others)
    {
      return gen.ShortIntegerOtherThan(others);
    }

    public static byte Digit()
    {
      return gen.Digit();
    }

    public static int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return gen.IntegerFromSequence(startingValue, step);
    }

    public static byte Octet()
    {
      return gen.Octet();
    }

    public static int IntegerDivisibleBy(int quotient)
    {
      return gen.IntegerDivisibleBy(quotient);
    }

    public static int IntegerNotDivisibleBy(int quotient)
    {
      return gen.IntegerNotDivisibleBy(quotient);
    }

    public static int IntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.IntegerWithExactDigitsCount(digitsCount);
    }

    public static long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public static uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return gen.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public static byte PositiveDigit()
    {
      return gen.PositiveDigit();
    }

  }
}
