﻿using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Math;

public class IntegerNotDivisibleByGenerator : InlineGenerator<int>
{
  private readonly IntegerDivisibleByGenerator _integerDivisibleByGenerator;

  public IntegerNotDivisibleByGenerator(int quotient)
  {
    AssertQuotientMakesSense(quotient);
    _integerDivisibleByGenerator = new IntegerDivisibleByGenerator(quotient);
  }

  public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _integerDivisibleByGenerator.GenerateInstance(instanceGenerator, request) + 1;
  }

  private static void AssertQuotientMakesSense(int quotient)
  {
    if (quotient == 1 || quotient == -1 || quotient == 0)
    {
      throw new ArgumentException($"generating an integer not divisible by {quotient} is not supported");
    }
  }
}
