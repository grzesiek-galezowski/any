using System;
using System.Linq;
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

  extension(Any)
  {
    public static T From<T>(params T[] possibleValues)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.From(possibleValues));
    }

    public static bool Boolean()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Boolean());
    }

    public static object Object()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Object());
    }

    public static T OtherThan<T>(params T[]? skippedValues)
    {
      if (skippedValues == null)
      {
        return Any.Instance<T>();
      }

      if (ThereAreRepeatedItemsIn(skippedValues))
      {
        throw new ArgumentException(
          "there is no point in passing a single value twice for skip",
          nameof(skippedValues));
      }

      if (ValueTypes.Contains(typeof(T)))
      {
        return Any.InstanceOf(InlineGenerators.InlineGenerators.ValueOtherThan(skippedValues));
      }
      else
      {
        return Any.InstanceOf(InlineGenerators.InlineGenerators.OtherThan(skippedValues));
      }
    }

    public static T Dummy<T>()
    {
      return Any.InstanceOf(new DummyGenerator<T>());
    }

    public static Guid Guid()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Guid());
    }

    public static Exception Exception()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Exception());
    }
  }

  private static bool ThereAreRepeatedItemsIn<T>(T[] omittedValues)
  {
    return !omittedValues.SequenceEqual(omittedValues.Distinct());
  }
}
