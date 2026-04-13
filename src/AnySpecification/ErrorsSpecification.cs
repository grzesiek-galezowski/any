using System;
using AwesomeAssertions;

namespace AnySpecification;

public class ErrorsSpecification
{
  [Test]
  public void ShouldIncludeGenerationLogInExceptionWhenItCannotGenerateAnObject()
  {
    this.Invoking(_ => Any.Instance<CannotGenerateThis>())
      .Should().Throw<Exception>().Which.ToString().Should().Contain(
        @"BUILT-IN ROOT: AnySpecification.CannotGenerateThis
 START: AnySpecification.CannotGenerateThis
 Resolution: TddXt.TypeResolution.FakeChainElements.FakeConcreteClass
 Picked constructor .ctor(NONE)
 END: AnySpecification.CannotGenerateThis");
  }

}

public class CannotGenerateThis
{
  public CannotGenerateThis()
  {
    throw new Exception("lol");
  }
}
