using System;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Strings
{
  public class StringFromCharsGenerator : InlineGenerator<string>
  {
    private readonly int _maxLength;
    private readonly InlineGenerator<char> _charGenerator;

    public StringFromCharsGenerator(int maxLength, InlineGenerator<char> charGenerator)
    {
      _maxLength = maxLength;
      _charGenerator = charGenerator;
    }

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var result = String.Empty;
      for (var i = 0; i < _maxLength; ++i)
      {
        result += _charGenerator.GenerateInstance(instanceGenerator, trace);
      }

      return result;
    }
  }
}