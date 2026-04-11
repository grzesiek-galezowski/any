using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class EnumerableConversion<TInput, TResult>(
  InlineGenerator<IEnumerable<TInput>> enumerableGenerator,
  Func<IEnumerable<TInput>, TResult> conversion)
  : InlineGenerator<TResult>
{
  public TResult GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return conversion(enumerableGenerator.GenerateInstance(instanceGenerator, request));
  }
}
