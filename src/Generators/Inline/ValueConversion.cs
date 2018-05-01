﻿using System;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{

  public class ValueConversion<TInput, TResult> : InlineGenerator<TResult>
  {
    private readonly InlineGenerator<TInput> _enumerableGenerator;
    private readonly Func<TInput, TResult> _conversion;

    public ValueConversion(
      InlineGenerator<TInput> enumerableGenerator,
      Func<TInput, TResult> conversion)
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