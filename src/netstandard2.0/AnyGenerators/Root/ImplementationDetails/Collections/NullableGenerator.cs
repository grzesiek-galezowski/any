using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Collections;

public class NullableGenerator<T> : InlineGenerator<T?> where T : struct
{
  public T? GenerateInstance(
    InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instanceGenerator.Instance<T>(request);
  }
}
