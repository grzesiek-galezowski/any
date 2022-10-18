using System;
using System.Threading;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Math;

public class NumberWithExactDigitNumberGenerator<T> : InlineGenerator<T>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));
  private readonly int _digitsCount;
  private readonly NumericTraits<T> _intTraits;

  public NumberWithExactDigitNumberGenerator(NumericTraits<T> intTraits, int digitsCount)
  {
    _intTraits = intTraits;
    _digitsCount = digitsCount;
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _intTraits.GenerateWithExactNumberOfDigits(_digitsCount, RandomGenerator.Value);
  }
}
