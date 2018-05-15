using System;
using System.Linq;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace Generators
{
  public class FixedValueGenerator<T> : InlineGenerator<T>
  {
    private readonly T _instance;

    public FixedValueGenerator(T instance)
    {
      _instance = instance;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _instance;
    }
  }
}