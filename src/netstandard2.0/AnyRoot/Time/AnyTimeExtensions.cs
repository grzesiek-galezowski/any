using System;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Time
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