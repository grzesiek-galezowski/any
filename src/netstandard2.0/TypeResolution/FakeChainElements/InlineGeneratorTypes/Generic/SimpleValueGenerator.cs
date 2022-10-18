using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class SimpleInstanceGenerator<T> : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.Instance<T>(request);
}
