using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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
    ClassicAssert.True(SmartType.For(exceptionType).IsException());
  }

  [Test, Parallelizable]
  public void ShouldReportWhenItIsNotDerivedFromException()
  {
    ClassicAssert.False(SmartType.For(typeof(string)).IsException());
  }

  [TestCase(9, true)]
  [TestCase(8, false)]
  public void ShouldReportWhetherTypeHasAtMostGivenConstructorCount(
    int maxCount, bool expectedResult)
  {
    var type = SmartType.For(typeof(string));
    ClassicAssert.AreEqual(expectedResult, type.HasPublicConstructorCountOfAtMost(maxCount));
  }
}
