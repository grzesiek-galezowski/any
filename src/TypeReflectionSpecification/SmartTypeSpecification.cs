using System;
using NUnit.Framework;
using TddXt.TypeReflection;

namespace TypeReflectionSpecification;

public class SmartTypeSpecification
{
  [TestCase(typeof(Exception))]
  [TestCase(typeof(ArgumentException))]
  [TestCase(typeof(InvalidCastException))]
  [TestCase(typeof(SystemException))]
  public void ShouldReportWhenItIsDerivedFromException(Type exceptionType)
  {
    Assert.That(SmartType.For(exceptionType).IsException(), Is.True);
  }

  [Test, Parallelizable]
  public void ShouldReportWhenItIsNotDerivedFromException()
  {
    Assert.That(SmartType.For(typeof(string)).IsException(), Is.False);
  }

  [TestCase(9, true)]
  [TestCase(8, false)]
  public void ShouldReportWhetherTypeHasAtMostGivenConstructorCount(
    int maxCount, bool expectedResult)
  {
    var type = SmartType.For(typeof(string));
    Assert.That(type.HasPublicConstructorCountOfAtMost(maxCount), Is.EqualTo(expectedResult));
  }
}
