using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Exploding
{
  public static class AnyExplodingExtensions
  {
    public static T Exploding<T>(this BasicGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Exploding<T>());
    }
  }
}