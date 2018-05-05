using Generators;
using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyNumberExtensions
  {
    public static int Integer(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Integer());
    }

    public static double Double(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Double());
    }

    public static long Long(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Long());
    }

    public static ulong UnsignedLong(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedLong());
    }

    public static byte Byte(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Byte());
    }

    public static decimal Decimal(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Decimal());
    }

    public static uint UnsignedInt(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedInt());
    }

    public static ushort UnsignedShort(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedShort());
    }

    public static short Short(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Short());
    }

    public static byte Digit(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Digit());
    }

    public static int IntegerFromSequence(this MyGenerator gen, int startingValue = 0, int step = 1)
    {
      return gen.InstanceOf(InlineGenerators.IntegerFromSequence(startingValue, step));
    }

    public static byte Octet(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Byte());
    }

    public static int IntegerDivisibleBy(this MyGenerator gen, int quotient)
    {
      return gen.InstanceOf(InlineGenerators.IntegerDivisibleBy(quotient));
    }

    public static int IntegerNotDivisibleBy(this MyGenerator gen, int quotient)
    {
      return gen.AllGenerator.IntegerNotDivisibleBy(quotient);
    }

    public static int IntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<int>(NumericTraits.Integer(), digitsCount).GenerateInstance(gen.AllGenerator);
    }

    public static long LongIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<long>(NumericTraits.Long(), digitsCount).GenerateInstance(gen.AllGenerator);
    }

    public static uint UnsignedIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<uint>(NumericTraits.UnsignedInteger(), digitsCount).GenerateInstance(gen.AllGenerator);
    }

    public static ulong UnsignedLongIntegerWithExactDigitsCount(this MyGenerator gen, int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<ulong>(NumericTraits.UnsignedLong(), digitsCount).GenerateInstance(gen.AllGenerator);
    }

    public static byte PositiveDigit(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.PositiveDigit());
    }

  }
}