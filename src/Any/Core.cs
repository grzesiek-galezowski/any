using Generators.ImplementationDetails;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore
{
  public class Core
  {
    public static BasicGenerator Any { get; } = AllGeneratorFactory.Create();
  }
}