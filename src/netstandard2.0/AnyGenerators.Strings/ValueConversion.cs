using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{

  public class ValueConversion<TInput, TResult> : InlineGenerator<TResult>
  {
    private readonly Func<TInput, TResult> _conversion;
    private readonly InlineGenerator<TInput> _enumerableGenerator;

    public ValueConversion(
      InlineGenerator<TInput> enumerableGenerator,
      Func<TInput, TResult> conversion)
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