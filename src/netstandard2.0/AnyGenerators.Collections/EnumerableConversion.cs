using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
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

    public TResult GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _conversion(_enumerableGenerator.GenerateInstance(instanceGenerator, trace));
    }
  }

}