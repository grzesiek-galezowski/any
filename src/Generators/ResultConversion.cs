using System;
using System.Collections.Generic;
using System.Linq;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class ResultConversion<TInput, TResult> : InlineGenerator<TResult>
  {
    private readonly InlineGenerator<IEnumerable<TInput>> _enumerableGenerator;
    private readonly Func<IEnumerable<TInput>, TResult> _conversion;

    public ResultConversion(
      InlineGenerator<IEnumerable<TInput>> enumerableGenerator,
      Func<IEnumerable<TInput>, TResult> conversion)
    {
      _enumerableGenerator = enumerableGenerator;
      _conversion = conversion;
    }

    public TResult GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _conversion(_enumerableGenerator.GenerateInstance(instanceGenerator));
    }
  }
}