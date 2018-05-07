using AnyCore;
using NSubstitute;
using NUnit.Framework;
using TddEbook.TddToolkitSpecification.Fixtures;
using TddToolkitSpecification.Fixtures;
using static AnyCore.Core;

namespace TddEbook.TddToolkitSpecification
{
  public class AnySubstituteSpecification
  {
    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndOverrideDefaultValues()
    {
      //GIVEN
      var instance = Any.Substitute<RecursiveInterface>();

      //WHEN
      var result = instance.Number;

      //THEN
      Assert.AreNotEqual(default(int), result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndNotOverrideStubbedValues()
    {
      //GIVEN
      var instance = Any.Substitute<RecursiveInterface>();
      instance.Number.Returns(44543);

      //WHEN
      var result = instance.Number;

      //THEN
      Assert.AreEqual(44543, result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndStillAllowVerifyingCalls()
    {
      //GIVEN
      var instance = Any.Substitute<RecursiveInterface>();

      //WHEN
      instance.VoidMethod();

      //THEN
      instance.Received(1).VoidMethod();
    }

    [Test]
    public void ShouldReturnNonNullImplementationsOfInnerObjects()
    {
      //GIVEN
      var instance = Any.Substitute<RecursiveInterface>();

      //WHEN
      var result = instance.Nested;

      //THEN
      Assert.NotNull(result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndSkipOverridingResultsStubbedWithNonDefaultValues()
    {
      var instance = Any.Substitute<RecursiveInterface>();
      var anotherInstance = Substitute.For<RecursiveInterface>();
      instance.Nested.Returns(anotherInstance);

      Assert.AreEqual(anotherInstance, instance.Nested);
    }

    [Test]
    public void ShouldBeAbleToBypassStaticCreationMethodWhenConstructorIsInternal()
    {
      Assert.DoesNotThrow(() => Any.Instance<FileExtension>());
      Assert.DoesNotThrow(() => Any.Instance<FileName>());
    }


    [Test]
    public void ShouldGenerateStringsContainingOtherObjects()
    {
      StringAssert.Contains("lol", Any.StringContaining("lol"));
      StringAssert.Contains("lol", Any.StringContaining<string>("lol"));
      StringAssert.Contains("2", Any.StringContaining(2));
      StringAssert.Contains("C", Any.StringContaining('C'));
    }
  }
}