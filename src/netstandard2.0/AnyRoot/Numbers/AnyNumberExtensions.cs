using System;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Numbers;

public static class AnyNumberExtensions
{
  public static int Integer(this BasicGenerator gen)
  {
    return gen.Instance<int>();
  }

  public static double Double(this BasicGenerator gen)
  {
    return gen.Instance<double>();
  }

  public static float Float(this BasicGenerator gen)
  {
    return gen.Instance<float>();
  }

  public static long Long(this BasicGenerator gen)
  {
    return gen.Instance<long>();
  }

  public static ulong UnsignedLong(this BasicGenerator gen)
  {
    return gen.Instance<ulong>();
  }

  public static byte Byte(this BasicGenerator gen)
  {
    return gen.Instance<byte>();
  }

  public static decimal Decimal(this BasicGenerator gen)
  {
    return gen.Instance<decimal>();
  }

  public static uint UnsignedInt(this BasicGenerator gen)
  {
    return gen.Instance<uint>();
  }

  public static ushort UnsignedShort(this BasicGenerator gen)
  {
    return gen.Instance<ushort>();
  }

  public static short Short(this BasicGenerator gen)
  {
    return gen.Instance<short>();
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

#if NET5_0_OR_GREATER
  public static Half Half(this BasicGenerator gen) => gen.Instance<Half>();
#endif
}
