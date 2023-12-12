using System;
using System.Threading;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Math;

public class NumberWithExactDigitNumberGenerator<T>(NumericTraits<T> intTraits, int digitsCount) : InlineGenerator<T>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return intTraits.GenerateWithExactNumberOfDigits(digitsCount, RandomGenerator.Value);
  }
}
