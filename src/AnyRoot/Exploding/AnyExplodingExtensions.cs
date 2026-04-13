namespace TddXt.AnyRoot.Exploding;

public static class AnyExplodingExtensions
{
  extension(Any)
  {
    public static T Exploding<T>() where T : class
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Exploding<T>());
    }
  }
}
