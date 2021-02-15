using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class StringFromCharsGenerator : InlineGenerator<string>
  {
    private readonly InlineGenerator<char> _charGenerator;
    private readonly int _maxLength;

    public StringFromCharsGenerator(int maxLength, InlineGenerator<char> charGenerator)
    {
      _maxLength = maxLength;
      _charGenerator = charGenerator;
    }

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var result = string.Empty;
      for (var i = 0; i < _maxLength; ++i)
      {
        result += _charGenerator.GenerateInstance(instanceGenerator, request);
      }

      return result;
    }
  }
}