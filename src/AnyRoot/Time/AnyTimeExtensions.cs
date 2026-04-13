using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Time;

public static class AnyTimeExtensions
{
  extension(BasicGenerator gen)
  {
    public DateTime DateTime()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.DateTime());
    }

    public DateTimeOffset DateTimeOffset()
    {
      return gen.Instance<DateTimeOffset>();
    }

    public TimeSpan TimeSpan()
    {
      return gen.InstanceOf(InlineGenerators.InlineGenerators.TimeSpan());
    }


    public DateOnly DateOnly()
    {
      return gen.Instance<DateOnly>();
    }

    public TimeOnly TimeOnly()
    {
      return gen.Instance<TimeOnly>();
    }
  }
}
