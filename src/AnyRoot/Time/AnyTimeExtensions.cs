using System;

namespace TddXt.AnyRoot.Time;

public static class AnyTimeExtensions
{
  extension(Any)
  {
    public static DateTime DateTime()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.DateTime());
    }

    public static DateTimeOffset DateTimeOffset()
    {
      return Any.Instance<DateTimeOffset>();
    }

    public static TimeSpan TimeSpan()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.TimeSpan());
    }


    public static DateOnly DateOnly()
    {
      return Any.Instance<DateOnly>();
    }

    public static TimeOnly TimeOnly()
    {
      return Any.Instance<TimeOnly>();
    }
  }
}
