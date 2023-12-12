using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class StringOfLengthGenerator(int length, InlineGenerator<string> stringGenerator) : InlineGenerator<string>
{
  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var result = string.Empty;
    while (result.Length < length)
    {
      result += stringGenerator.GenerateInstance(instanceGenerator, request);
    }

    return result.Substring(0, length);
  }
}
