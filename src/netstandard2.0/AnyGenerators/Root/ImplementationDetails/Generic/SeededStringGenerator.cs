using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Generic;

public class SeededStringGenerator : InlineGenerator<string>
{
  private readonly string _seed;

  public SeededStringGenerator(string seed)
  {
    _seed = seed;
  }

  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _seed + instanceGenerator.Instance<string>(request);
  }
}
