using TddXt.AnyExtensibility;
using TddXt.TypeResolution;

namespace TddXt.AnyRoot;

public static class Root
{
  public static BasicGenerator Any { get; } = AllGeneratorFactory.Create();
}
