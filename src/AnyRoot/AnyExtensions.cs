using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

namespace TddXt.AnyRoot;

public static class AnyExtensions
{
  private static readonly Type[] ValueTypes =
  [
    typeof(byte),
    typeof(short),
    typeof(ushort),
    typeof(decimal),
    typeof(int),
    typeof(uint),
    typeof(long),
    typeof(ulong),
    typeof(float),
    typeof(double),
    typeof(bool),
    typeof(sbyte),
    typeof(char),
    typeof(object),
    typeof(string)
  ];

  extension(BasicGenerator gen)
  {
    public T From<T>(params T[] possibleValues)
    {
      return gen.InstanceOf(InlineGenerators.From(possibleValues));
    }

    public bool Boolean()
    {
      return gen.InstanceOf(InlineGenerators.Boolean());
    }

    public object Object()
    {
      return gen.InstanceOf(InlineGenerators.Object());
    }

    public T OtherThan<T>(params T[]? skippedValues)
    {
      if (skippedValues == null)
      {
        return gen.Instance<T>();
      }

      if (ThereAreRepeatedItemsIn(skippedValues))
      {
        throw new ArgumentException(
          "there is no point in passing a single value twice for skip",
          nameof(skippedValues));
      }

      if (ValueTypes.Contains(typeof(T)))
      {
        return gen.InstanceOf(InlineGenerators.ValueOtherThan(skippedValues));
      }
      else
      {
        return gen.InstanceOf(InlineGenerators.OtherThan(skippedValues));
      }
    }

    public T Dummy<T>()
    {
      return gen.InstanceOf(new DummyGenerator<T>());
    }

    public Guid Guid()
    {
      return gen.InstanceOf(InlineGenerators.Guid());
    }

    public Exception Exception()
    {
      return gen.InstanceOf(InlineGenerators.Exception());
    }
  }

  private static bool ThereAreRepeatedItemsIn<T>(T[] omittedValues)
  {
    return !omittedValues.SequenceEqual(omittedValues.Distinct());
  }
}
