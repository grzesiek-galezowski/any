using NSubstitute;
using NUnit.Framework;
using TddToolkitSpecification.Fixtures;
using TddXt.AnyRoot;
using TddXt.AnyRoot.NSubstitute;
using TddXt.AnyRoot.Strings;

namespace TddToolkitSpecification
{
  public class AnySubstituteSpecification
  {
    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndOverrideDefaultValues()
    {
      //GIVEN
      var instance = Root.Any.Substitute<RecursiveInterface>();

      //WHEN
      var result = instance.Number;

      //THEN
      Assert.AreNotEqual(default(int), result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndNotOverrideStubbedValues()
    {
      //GIVEN
      var instance = Root.Any.Substitute<RecursiveInterface>();
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
      var instance = Root.Any.Substitute<RecursiveInterface>();

      //WHEN
      instance.VoidMethod();

      //THEN
      instance.Received(1).VoidMethod();
    }

    [Test]
    public void ShouldReturnNonNullImplementationsOfInnerObjects()
    {
      //GIVEN
      var instance = Root.Any.Substitute<RecursiveInterface>();

      //WHEN
      var result = instance.Nested;

      //THEN
      Assert.NotNull(result);
    }

    [Test]
    public void ShouldBeAbleToWrapSubstitutesAndSkipOverridingResultsStubbedWithNonDefaultValues()
    {
      var instance = Root.Any.Substitute<RecursiveInterface>();
      var anotherInstance = Substitute.For<RecursiveInterface>();
      instance.Nested.Returns(anotherInstance);

      Assert.AreEqual(anotherInstance, instance.Nested);
    }

    [Test]
    public void ShouldBeAbleToBypassStaticCreationMethodWhenConstructorIsInternal()
    {
      Assert.DoesNotThrow(() => Root.Any.Instance<FileExtension>());
      Assert.DoesNotThrow(() => Root.Any.Instance<FileName>());
    }


    [Test]
    public void ShouldGenerateStringsContainingOtherObjects()
    {
      StringAssert.Contains("lol", Root.Any.StringContaining("lol"));
      StringAssert.Contains("lol", Root.Any.StringContaining<string>("lol"));
      StringAssert.Contains("2", Root.Any.StringContaining(2));
      StringAssert.Contains("C", Root.Any.StringContaining('C'));
    }
  }
}