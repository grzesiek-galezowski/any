using System;
using TddEbook.TddToolkit.Generators;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore.Time
{
  public static class AnyTimeExtensions
  {
    public static DateTime DateTime(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.DateTime());
    }

    public static TimeSpan TimeSpan(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.TimeSpan());
    }
  }
}