using System;
using System.Linq;
using FluentAssertions;
using static TddXt.AnyGenerators.Root.InlineGenerators;
using Enumerable = System.Linq.Enumerable;

namespace AnySpecification;

public class RangeGeneratorSpecification
{
  private const int Count = 100;

  [Test]
  public void ShouldGenerateIntsInRange()
  {
    var count = Count;
    var min = int.MinValue;
    var max = min + count - 1;
    Console.WriteLine("begin");
    Enumerable.Range(0, count).Select(_ => Any.InstanceOf(IntegerInRange(min, max))).ToList()
      .Should().HaveCount(count).And.OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
    
    Enumerable.Range(0, count * 2)
      .Select(_ => Any.InstanceOf(IntegerInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));
  }

  [Test]
  public void ShouldGenerateUnsignedIntsInRange()
  {
    var min = uint.MinValue;
    var max = min + Count - 1;

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(UnsignedIntInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(UnsignedIntInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));
  }

  [Test]
  public void ShouldGenerateShortsInRange()
  {
    var min = short.MinValue;
    var max = (short)(min + Count - 1);

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(ShortInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(ShortInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));

  }

  [Test]
  public void ShouldGenerateUnsignedShortsInRange()
  {
    var min = ushort.MinValue;
    var max = (ushort)(min + Count - 1);

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(UnsignedShortInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(UnsignedShortInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));

  }

  [Test]
  public void ShouldGenerateLongsInRange()
  {
    var min = long.MinValue;
    var max = min + Count - 1;

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(LongInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(LongInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));
  }

  [Test]
  public void ShouldGenerateUnsignedLongsInRange()
  {
    var min = ulong.MinValue;
    var max = min + Count - 1;

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(UnsignedLongInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(UnsignedLongInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));
  }

  [Test]
  public void ShouldGenerateBytesInRange()
  {
    var min = byte.MinValue;
    var max = (byte)(min + Count - 1);

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(ByteInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(ByteInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));

  }

  [Test]
  public void ShouldGenerateSignedBytesInRange()
  {
    var min = sbyte.MinValue;
    var max = (sbyte)(min + Count - 1);

    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(SignedByteInRange(min, max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(SignedByteInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));

  }

  [Test]
  public void ShouldGenerateDecimalsInRange()
  {
    var min = decimal.MinValue;
    var max = min + Count - 1;
    Enumerable.Range(0, Count).Select(_ => Any.InstanceOf(DecimalInRange(min, max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));

    Enumerable.Range(0, Count * 2)
      .Select(_ => Any.InstanceOf(DecimalInRange(min,max)))
      .GroupBy(n => n)
      .ToList()
      .Should().AllSatisfy(g => g.Should().HaveCount(2));
  }
}
