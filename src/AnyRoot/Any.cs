using TddXt.AnyExtensibility;
using TddXt.TypeResolution;

namespace TddXt.AnyRoot;

public static class Any
{
  private static BasicGenerator generator { get; } = AllGeneratorFactory.Create();

  public static T Instance<T>() => generator.Instance<T>();
  public static T Instance<T>(params GenerationCustomization[] customizations) => generator.Instance<T>(customizations);
  public static T InstanceOf<T>(InlineGenerator<T> gen) => generator.InstanceOf(gen);
}
