using System.Globalization;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class IdentifierStringGenerator(InlineGenerator<char> digitChar, InlineGenerator<char> alphaChar)
  : InlineGenerator<string>
{
  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    string result = alphaChar.GenerateInstance(instanceGenerator, request).ToString(CultureInfo.InvariantCulture);
    for (var i = 0; i < 5; ++i)
    {
      result += digitChar.GenerateInstance(instanceGenerator, request);
      result += alphaChar.GenerateInstance(instanceGenerator, request);
    }

    return result;
  }
}
