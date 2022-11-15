using System;
using System.Linq;
using FluentAssertions;
using TddXt.AnyRoot.Time;

namespace AnySpecification;

public class TimeTypesSpecification
{
  [Test, Parallelizable]
  public void ShouldGenerateDifferentDateTimeEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<DateTime>())
      .Should().OnlyHaveUniqueItems();
    Enumerable.Range(1, 1000)
      .Select(n => Any.DateTime())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentDateTimeOffsetEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<DateTimeOffset>())
      .Should().OnlyHaveUniqueItems();
    Enumerable.Range(1, 1000)
      .Select(n => Any.DateTimeOffset())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentTimeSpanEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<TimeSpan>())
      .Should().OnlyHaveUniqueItems();
    Enumerable.Range(1, 1000)
      .Select(n => Any.TimeSpan())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentDateOnlyEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<DateOnly>())
      .Should().OnlyHaveUniqueItems();
    Enumerable.Range(1, 1000)
      .Select(n => Any.DateOnly())
      .Should().OnlyHaveUniqueItems();
  }

  [Test, Parallelizable]
  public void ShouldGenerateDifferentTimeOnlyEachTime()
  {
    Enumerable.Range(1, 1000)
      .Select(n => Any.Instance<TimeOnly>())
      .Should().OnlyHaveUniqueItems();
    Enumerable.Range(1, 1000)
      .Select(n => Any.TimeOnly())
      .Should().OnlyHaveUniqueItems();
  }

}
