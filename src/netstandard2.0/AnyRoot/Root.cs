using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;

namespace TddXt.AnyRoot
{
  public static class Root
  {
    public static BasicGenerator Any { get; } = AllGeneratorFactory.Create();
  }
}