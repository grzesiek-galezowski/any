using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Collections;

public class EnumerableConversion<TInput, TResult> : InlineGenerator<TResult>
{
  private readonly Func<IEnumerable<TInput>, TResult> _conversion;
  private readonly InlineGenerator<IEnumerable<TInput>> _enumerableGenerator;

  public EnumerableConversion(
    InlineGenerator<IEnumerable<TInput>> enumerableGenerator,
    Func<IEnumerable<TInput>, TResult> conversion)
  {
    _enumerableGenerator = enumerableGenerator;
    _conversion = conversion;
  }

  public TResult GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _conversion(_enumerableGenerator.GenerateInstance(instanceGenerator, request));
  }
}