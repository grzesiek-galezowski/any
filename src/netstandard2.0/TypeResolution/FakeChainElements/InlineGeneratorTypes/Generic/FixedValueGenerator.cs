using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class FixedValueGenerator<T>(T instance) : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instance;
  }
}
