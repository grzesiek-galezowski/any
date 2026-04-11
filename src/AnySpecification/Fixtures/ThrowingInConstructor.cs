using System;

namespace AnySpecification.Fixtures;

public class ThrowingInConstructor
{
  public ThrowingInConstructor()
  {
    throw new Exception();
  }
}