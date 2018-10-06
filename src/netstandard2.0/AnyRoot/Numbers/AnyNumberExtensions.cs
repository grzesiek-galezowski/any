using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Numbers
{
  public static class AnyNumberExtensions
  {
    public static int Integer(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Integer());
    }

    public static double Double(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Double());
    }

    public static long Long(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Long());
    }

    public static ulong UnsignedLong(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedLong());
    }

    public static byte Byte(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Byte());
    }

    public static decimal Decimal(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Decimal());
    }

    public static uint UnsignedInt(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedInt());
    }

    public static ushort UnsignedShort(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UnsignedShort());
    }

    public static short Short(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Short());
    }

    public static byte Digit(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Digit());
    }

    public static int IntegerFromSequence(this BasicGenerator gen, int startingValue = 0, int step = 1)
    {
      return gen.InstanceOf(InlineGenerators.IntegerFromSequence(startingValue, step));
    }

    public static byte PositiveDigit(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.PositiveDigit());
    }
  }
}