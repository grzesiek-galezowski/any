using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Exploding;

public static class AnyExplodingExtensions
{
  extension(BasicGenerator gen)
  {
    public T Exploding<T>() where T : class
    {
      return gen.InstanceOf(InlineGenerators.Exploding<T>());
    }
  }
}