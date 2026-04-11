using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class ValueConversion<TInput, TResult>(
  InlineGenerator<TInput> enumerableGenerator,
  Func<TInput, TResult> conversion)
  : InlineGenerator<TResult>
{
  public TResult GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return conversion(enumerableGenerator.GenerateInstance(instanceGenerator, request));
  }
}
