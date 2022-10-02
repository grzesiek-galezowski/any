using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic;

public class SimpleInstanceGenerator<T> : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.Instance<T>(request);
}
