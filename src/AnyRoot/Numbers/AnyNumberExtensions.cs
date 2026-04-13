using System;

namespace TddXt.AnyRoot.Numbers;

public static class AnyNumberExtensions
{
  extension(Any)
  {
    public static int Integer() => Any.Instance<int>();

    public static double Double() => Any.Instance<double>();

    public static float Float() => Any.Instance<float>();

    public static long Long() => Any.Instance<long>();

    public static ulong UnsignedLong() => Any.Instance<ulong>();

    public static byte Byte() => Any.Instance<byte>();

    public static decimal Decimal() => Any.Instance<decimal>();

    public static uint UnsignedInt() => Any.Instance<uint>();

    public static ushort UnsignedShort() => Any.Instance<ushort>();

    public static short Short() => Any.Instance<short>();

    public static byte Digit() => Any.InstanceOf(InlineGenerators.InlineGenerators.Digit());

    public static int IntegerFromSequence(int startingValue = 0, int step = 1) => Any.InstanceOf(InlineGenerators.InlineGenerators.IntegerFromSequence(startingValue, step));

    public static byte PositiveDigit() => Any.InstanceOf(InlineGenerators.InlineGenerators.PositiveDigit());

    public static Half Half() => Any.Instance<Half>();
  }
}
