using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Numbers;

public static class AnyNumberExtensions
{
  extension(BasicGenerator gen)
  {
    public int Integer() => gen.Instance<int>();

    public double Double() => gen.Instance<double>();

    public float Float() => gen.Instance<float>();

    public long Long() => gen.Instance<long>();

    public ulong UnsignedLong() => gen.Instance<ulong>();

    public byte Byte() => gen.Instance<byte>();

    public decimal Decimal() => gen.Instance<decimal>();

    public uint UnsignedInt() => gen.Instance<uint>();

    public ushort UnsignedShort() => gen.Instance<ushort>();

    public short Short() => gen.Instance<short>();

    public byte Digit() => gen.InstanceOf(InlineGenerators.InlineGenerators.Digit());

    public int IntegerFromSequence(int startingValue = 0, int step = 1) => gen.InstanceOf(InlineGenerators.InlineGenerators.IntegerFromSequence(startingValue, step));

    public byte PositiveDigit() => gen.InstanceOf(InlineGenerators.InlineGenerators.PositiveDigit());

    public Half Half() => gen.Instance<Half>();
  }
}
