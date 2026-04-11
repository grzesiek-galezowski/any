using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class StringFromCharsGenerator(int maxLength, InlineGenerator<char> charGenerator) : InlineGenerator<string>
{
  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var result = string.Empty;
    for (var i = 0; i < maxLength; ++i)
    {
      result += charGenerator.GenerateInstance(instanceGenerator, request);
    }

    return result;
  }
}
