using System;
using System.Numerics;
using System.Text;

namespace TddXt.AnyGenerators.Math
{
  public static class NumericTraits
  {
    private static readonly NumericTraits<int> IntTraits = new NumericTraits<int>(int.MaxValue, bi => (int) bi);
    private static readonly NumericTraits<long> LongTraits = new NumericTraits<long>(long.MaxValue, bi => (long) bi);
    private static readonly NumericTraits<uint> UintTraits = new NumericTraits<uint>(uint.MaxValue, bi => (uint) bi);
    private static readonly NumericTraits<ulong> UlongTraits = new NumericTraits<ulong>(ulong.MaxValue, bi => (ulong) bi);

    public static NumericTraits<int> Integer()
    {
      return IntTraits;
    }

    public static NumericTraits<long> Long()
    {
      return LongTraits;
    }

    public static NumericTraits<uint> UnsignedInteger()
    {
      return UintTraits;
    }

    public static NumericTraits<ulong> UnsignedLong()
    {
      return UlongTraits;
    }
  }
  public class NumericTraits<T>
  {
    private readonly Func<BigInteger, T> _cast;

    public NumericTraits(BigInteger maxValue, Func<BigInteger, T> cast)
    {
      _cast = cast;
      Max = maxValue;
      MaxValueString = Max.ToString();
      MaxPossibleDigitsCount = MaxValueString.Length;
    }


    private BigInteger Max { get; }
    private int MaxPossibleDigitsCount { get; }
    private string MaxValueString { get; }

    public T GenerateWithExactNumberOfDigits(int digitsCount, Random randomGenerator)
    {
      if (digitsCount > MaxPossibleDigitsCount)
      {
        throw new ArgumentOutOfRangeException(nameof(digitsCount), digitsCount,
          $"expected at most {MaxPossibleDigitsCount}");
      }
      var bytes = GetRandomDigits(digitsCount, randomGenerator);
      var bigInteger = NarrowDownToSpecificNumericTypeRange(bytes);
      return _cast.Invoke(bigInteger);
    }

    private static string GetRandomDigits(int digitsCount, Random randomGenerator)
    {
      var str = "";
      str += randomGenerator.Next(1, 9);
      var builder = new StringBuilder();
      builder.Append(str);
      for (int i = 1; i < digitsCount; i++)
      {
        builder.Append(randomGenerator.Next(0, 9));
      }
      str = builder.ToString();
      return str;
    }

    private static BigInteger MinimumValueWithSpecifiedDigits(int length)
    {
      var result = "1";
      var builder = new StringBuilder();
      builder.Append(result);
      for (int i = 1; i < length; ++i)
      {
        builder.Append(@"0");
      }
      result = builder.ToString();
      return BigInteger.Parse(result);
    }

    private BigInteger NarrowDownToSpecificNumericTypeRange(string number)
    {
      var bigInteger = BigInteger.Parse(number);
      var min = MinimumValueWithSpecifiedDigits(number.Length);
      var narrowDownToSpecificNumericTypeRange = bigInteger % (Max - min) + min;
      return narrowDownToSpecificNumericTypeRange;
    }
  }
}