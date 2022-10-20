using System.Linq;
using FluentAssertions;
using static TddXt.AnyGenerators.Root.InlineGenerators;
using Enumerable = System.Linq.Enumerable;

namespace AnySpecification;

public class RangeGeneratorSpecification
{
  [Test]
  public void ShouldGenerateIntsInRange()
  {
    var min = int.MinValue;
    var max = min + 100;

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(IntegerInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateUnsignedIntsInRange()
  {
    var min = uint.MinValue;
    var max = min + 100;

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(UnsignedIntInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateShortsInRange()
  {
    var min = short.MinValue;
    var max = (short)(min + 100);

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(ShortInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateUnsignedShortsInRange()
  {
    var min = ushort.MinValue;
    var max = (ushort)(min + 100);

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(UnsignedShortInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateLongsInRange()
  {
    var min = long.MinValue;
    var max = min + 100;

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(LongInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateUnsignedLongsInRange()
  {
    var min = ulong.MinValue;
    var max = min + 100;

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(UnsignedLongInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateBytesInRange()
  {
    var min = byte.MinValue;
    var max = (byte)(min + 100);

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(ByteInRange(min,max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateSignedBytesInRange()
  {
    var min = sbyte.MinValue;
    var max = (sbyte)(min + 100);

    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(SignedByteInRange(min, max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }

  [Test]
  public void ShouldGenerateDecimalsInRange()
  {
    var min = decimal.MinValue;
    var max = min + 100;
    Enumerable.Range(0, 100).Select(_ => Any.InstanceOf(DecimalInRange(min, max)))
      .Should().OnlyHaveUniqueItems()
      .And.AllSatisfy(n => n.Should().BeGreaterThanOrEqualTo(min))
      .And.AllSatisfy(n => n.Should().BeLessThanOrEqualTo(max));
  }
}
