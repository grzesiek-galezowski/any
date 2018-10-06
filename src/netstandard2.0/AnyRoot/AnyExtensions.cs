using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot
{
  public static class AnyExtensions
  {
    private static readonly Type[] ValueTypes = {
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
    };


    public static T From<T>(this BasicGenerator gen, params T[] possibleValues)
    {
      return gen.InstanceOf(InlineGenerators.From(possibleValues));
    }

    public static bool Boolean(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Boolean());
    }

    public static object Object(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Object());
    }

    public static T OtherThan<T>(this BasicGenerator gen, params T[] omittedValues)
    {
      if (ValueTypes.Contains(typeof(T)))
      {
        return gen.InstanceOf(InlineGenerators.ValueOtherThan(omittedValues)); 
      }
      else
      {
        return gen.InstanceOf(InlineGenerators.OtherThan(omittedValues));
      }
    }

    public static T Dummy<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(new DummyGenerator<T>());
    }


    public static Guid Guid(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Guid());
    }

    public static Exception Exception(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Exception());
    }
  }
}