using Generators;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore.NSubstitute
{
  public static class AnyNSubstituteExtensions
  {
    public static T Substitute<T>(this BasicGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Substitute<T>());
    }
  }
}