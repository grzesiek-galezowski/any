using System;

namespace AnySpecification.Fixtures;

public class ObjectWithStaticParseMethod
{
  private ObjectWithStaticParseMethod(int x)
  {
    X = x;
  }

  public int X { get; set; }

  public static ObjectWithStaticParseMethod ParseInt(int x)
  {
    throw new Exception("Thou shalt not pass!");
  }
}
