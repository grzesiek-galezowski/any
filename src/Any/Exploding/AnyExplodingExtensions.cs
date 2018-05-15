using TddEbook.TddToolkit.Generators;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore.Exploding
{
  public static class AnyExplodingExtensions
  {
    public static T Exploding<T>(this BasicGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Exploding<T>());
    }
  }
}