namespace TddXt.AnyExtensibility;

public static class ManyStrategies
{
  public static ManyStrategy FromConstant(int length) => new UseConstantValueStrategy(length);

  public static ManyStrategy FromRequest()
  {
    return new FromRequestStrategy();
  }
}