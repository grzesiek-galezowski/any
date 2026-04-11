using System;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Numbers;

public static class AnyNumberExtensions
{
  extension(BasicGenerator gen)
  {
    public int Integer()
    {
      return gen.Instance<int>();
    }

    public double Double()
    {
      return gen.Instance<double>();
    }

    public float Float()
    {
      return gen.Instance<float>();
    }

    public long Long()
    {
      return gen.Instance<long>();
    }

    public ulong UnsignedLong()
    {
      return gen.Instance<ulong>();
    }

    public byte Byte()
    {
      return gen.Instance<byte>();
    }

    public decimal Decimal()
    {
      return gen.Instance<decimal>();
    }

    public uint UnsignedInt()
    {
      return gen.Instance<uint>();
    }

    public ushort UnsignedShort()
    {
      return gen.Instance<ushort>();
    }

    public short Short()
    {
      return gen.Instance<short>();
    }

    public byte Digit()
    {
      return gen.InstanceOf(InlineGenerators.Digit());
    }

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return gen.InstanceOf(InlineGenerators.IntegerFromSequence(startingValue, step));
    }

    public byte PositiveDigit()
    {
      return gen.InstanceOf(InlineGenerators.PositiveDigit());
    }

    public Half Half() => gen.Instance<Half>();
  }
}
