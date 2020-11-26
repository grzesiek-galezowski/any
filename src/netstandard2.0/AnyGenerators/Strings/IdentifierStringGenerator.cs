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

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      string result = _alphaChar.GenerateInstance(instanceGenerator, trace).ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += _digitChar.GenerateInstance(instanceGenerator, trace);
        result += _alphaChar.GenerateInstance(instanceGenerator, trace);
      }

      return result;
    }
  }
}