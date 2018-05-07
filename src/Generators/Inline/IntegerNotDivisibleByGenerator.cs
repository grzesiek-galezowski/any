using System;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class IntegerNotDivisibleByGenerator : InlineGenerator<int>
  {
    private readonly IntegerDivisableByGenerator _integerDivisableByGenerator;

    public IntegerNotDivisibleByGenerator(int quotient)
    {
      AssertQuotientMakesSense(quotient);
      _integerDivisableByGenerator = new IntegerDivisableByGenerator(quotient);
    }

    public int GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _integerDivisableByGenerator.GenerateInstance(instanceGenerator) + 1;
    }

    private static void AssertQuotientMakesSense(int quotient)
    {
      if (quotient == 1 || quotient == -1 || quotient == 0)
      {
        throw new ArgumentException($"generating an integer not divisible by {quotient} is not supported");
      }
    }
  }
}