using System;
using Generators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class NumericGenerator
  {
    public int IntegerNotDivisibleBy(int quotient, InstanceGenerator allGenerator)
    {
      AssertQuotientMakesSense(quotient);
      return new IntegerDivisableByGenerator(quotient).GenerateInstance(allGenerator) + 1;
    }

    private void AssertQuotientMakesSense(int quotient)
    {
      if (quotient == 1 || quotient == -1 || quotient == 0)
      {
        throw new ArgumentException($"generating an integer not dividable by {quotient} is not supported");
      }
    }

    public static byte Octet2(ValueGenerator valueGenerator)
    {
      return valueGenerator.ValueOf<byte>();
    }
  }

}