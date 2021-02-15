using System.Globalization;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class IdentifierStringGenerator : InlineGenerator<string>
  {
    private readonly InlineGenerator<char> _alphaChar;
    private readonly InlineGenerator<char> _digitChar;

    public IdentifierStringGenerator(InlineGenerator<char> digitChar, InlineGenerator<char> alphaChar)
    {
      _digitChar = digitChar;
      _alphaChar = alphaChar;
    }

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      string result = _alphaChar.GenerateInstance(instanceGenerator, request).ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += _digitChar.GenerateInstance(instanceGenerator, request);
        result += _alphaChar.GenerateInstance(instanceGenerator, request);
      }

      return result;
    }
  }
}