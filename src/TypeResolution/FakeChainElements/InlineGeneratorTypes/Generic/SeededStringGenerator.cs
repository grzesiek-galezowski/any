using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class SeededStringGenerator(string seed) : InlineGenerator<string>
{
  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return seed + instanceGenerator.Instance<string>(request);
  }
}
