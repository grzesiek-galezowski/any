using System.Globalization;
using TddXt.AnyExtensibility;

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

    public string GenerateInstance(InstanceGenerator instanceGenerator)
    {
      string result = _alphaChar.GenerateInstance(instanceGenerator).ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += _digitChar.GenerateInstance(instanceGenerator);
        result += _alphaChar.GenerateInstance(instanceGenerator);
      }

      return result;
    }
  }
}