using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class SimpleInstanceOtherThanGenerator<T>(T[] excluded) : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.OtherThan(excluded);
}
