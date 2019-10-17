using System;

namespace TddToolkitSpecification.Fixtures
{
  public class ThrowingInConstructor
  {
    public ThrowingInConstructor()
    {
      throw new Exception();
    }
  }
}