using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;

namespace TddXt.AnyRoot
{
  public class Root
  {
    public static BasicGenerator Any { get; } = AllGeneratorFactory.Create();
  }
}