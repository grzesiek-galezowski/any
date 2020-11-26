using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Math
{
  public class IntegerNotDivisibleByGenerator : InlineGenerator<int>
  {
    private readonly IntegerDivisibleByGenerator _integerDivisibleByGenerator;

    public IntegerNotDivisibleByGenerator(int quotient)
    {
      AssertQuotientMakesSense(quotient);
      _integerDivisibleByGenerator = new IntegerDivisibleByGenerator(quotient);
    }

    public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _integerDivisibleByGenerator.GenerateInstance(instanceGenerator, trace) + 1;
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