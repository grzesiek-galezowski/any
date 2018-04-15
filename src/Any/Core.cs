namespace AnyCore
{
  public class Core
  {
    public static OmniGenerator Any { get; } = new OmniGenerator();
  }

  public static class AnyExtentions
  {
    public static int Integer(this OmniGenerator generator)
    {
      return 213;
    }
  }


  public class OmniGenerator
  {
  }
}
