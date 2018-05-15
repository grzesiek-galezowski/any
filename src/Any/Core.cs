using TddEbook.TddToolkit.Subgenerators;
using TddXt.AnyExtensibility;

namespace TddXt.AnyCore
{
  public class Core
  {
    public static BasicGenerator Any { get; } = AllGeneratorFactory.Create();
  }
}