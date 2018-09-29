using System.Globalization;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Strings
{
  public class IdentifierStringGenerator : InlineGenerator<string>
  {
    private readonly InlineGenerator<char> _digitChar;
    private readonly InlineGenerator<char> _alphaChar;

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