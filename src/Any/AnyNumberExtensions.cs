using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyNumberExtensions
  {
    public static int Integer(this MyGenerator gen)
    {
      return gen.AllGenerator.Integer();
    }

    public static double Double(this MyGenerator gen)
    {
      return gen.AllGenerator.Double();
    }

    public static double DoubleOtherThan(this MyGenerator gen, params double[] others)
    {
      return gen.AllGenerator.DoubleOtherThan(others);
    }

    public static long LongInteger(this MyGenerator gen)
    {
      return gen.AllGenerator.LongInteger();
    }

    public static long LongIntegerOtherThan(this MyGenerator gen, params long[] others)
    {
      return gen.AllGenerator.LongIntegerOtherThan(others);  //todo are these needed?
    }

    public static ulong UnsignedLongInteger(this MyGenerator gen)
    {
      return gen.AllGenerator.UnsignedLongInteger();
    }

    public static ulong UnsignedLongIntegerOtherThan(this MyGenerator gen, params ulong[] others)
    {
      return gen.AllGenerator.UnsignedLongIntegerOtherThan(others);
    }

    public static int IntegerOtherThan(this MyGenerator gen, params int[] others)
    {
      return gen.AllGenerator.IntegerOtherThan(others);
    }

    public static byte Byte(this MyGenerator gen)
    {
      return gen.AllGenerator.Byte();
    }

    public static byte ByteOtherThan(this MyGenerator gen, params byte[] others)
    {
      return gen.AllGenerator.ByteOtherThan(others);
    }

    public static decimal Decimal(this MyGenerator gen)
    {
      return gen.AllGenerator.Decimal();
    }

    public static decimal DecimalOtherThan(this MyGenerator gen, params decimal[] others)
    {
      return gen.AllGenerator.DecimalOtherThan(others);
    }

    public static uint UnsignedInt(this MyGenerator gen)
    {
      return gen.AllGenerator.UnsignedInt();
    }

    public static uint UnsignedIntOtherThan(this MyGenerator gen, params uint[] others)
    {
      return gen.AllGenerator.UnsignedIntOtherThan(others);
    }

    public static ushort UnsignedShort(this MyGenerator gen)
    {
      return gen.AllGenerator.UnsignedShort();
    }

    public static ushort UnsignedShortOtherThan(this MyGenerator gen, params ushort[] others)
    {
      return gen.AllGenerator.UnsignedShortOtherThan(others);
    }

    public static short ShortInteger(this MyGenerator gen)
    {
      return gen.AllGenerator.ShortInteger();
    }

    public static short ShortIntegerOtherThan(this MyGenerator gen, params short[] others)
    {
      return gen.AllGenerator.ShortIntegerOtherThan(others);
    }

    public static byte Digit(this MyGenerator gen)
    {
      return gen.AllGenerator.Digit();
    }

    public static int IntegerFromSequence(this MyGenerator gen, int startingValue = 0, int step = 1)
    {
      return gen.AllGenerator.IntegerFromSequence(startingValue, step);
    }

    public static byte Octet(this MyGenerator gen)
    {
      return gen.AllGenerator.Octet();
    }

    public static int IntegerDivisibleBy(this MyGenerator gen, int quotient)
    {
      return gen.AllGenerator.IntegerDivisibleBy(quotient);
    }

    public static int IntegerNotDivisibleBy(this MyGenerator gen, int quotient)
    {
      return gen.AllGenerator.IntegerNotDivisibleBy(quotient);
    }

    public static int IntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return gen.AllGenerator.IntegerWithExactDigitsCount(digitsCount);
    }

    public static long LongIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return gen.AllGenerator.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public static uint UnsignedIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return gen.AllGenerator.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return gen.AllGenerator.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public static byte PositiveDigit(this MyGenerator gen)
    {
      return gen.AllGenerator.PositiveDigit();
    }

  }
}