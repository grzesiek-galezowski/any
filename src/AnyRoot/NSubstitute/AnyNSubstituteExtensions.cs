using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.NSubstitute
{
  public static class AnyNSubstituteExtensions
  {
    public static T Substitute<T>(this BasicGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Substitute<T>());
    }
  }
}