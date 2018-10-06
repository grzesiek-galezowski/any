using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class StringOfLengthGenerator : InlineGenerator<string>
  {
    private readonly int _length;
    private readonly InlineGenerator<string> _stringGenerator;

    public StringOfLengthGenerator(int length, InlineGenerator<string> stringGenerator)
    {
      _length = length;
      _stringGenerator = stringGenerator;
    }

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var result = String.Empty;
      while (result.Length < _length)
      {
        result += _stringGenerator.GenerateInstance(instanceGenerator, trace);
      }

      return result.Substring(0, _length);
    }
  }
}