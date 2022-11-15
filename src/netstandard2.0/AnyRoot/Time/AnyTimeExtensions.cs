using System;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Time;

public static class AnyTimeExtensions
{
  public static DateTime DateTime(this BasicGenerator gen)
  {
    return gen.InstanceOf(InlineGenerators.DateTime());
  }
  
  public static DateTimeOffset DateTimeOffset(this BasicGenerator gen)
  {
    return gen.Instance<DateTimeOffset>();
  }

  public static TimeSpan TimeSpan(this BasicGenerator gen)
  {
    return gen.InstanceOf(InlineGenerators.TimeSpan());
  }


  #if NET6_0_OR_GREATER
  public static DateOnly DateOnly(this BasicGenerator gen)
  {
    return gen.Instance<DateOnly>();
  }

  public static TimeOnly TimeOnly(this BasicGenerator gen)
  {
      return gen.Instance<TimeOnly>();
  }
  #endif

}
