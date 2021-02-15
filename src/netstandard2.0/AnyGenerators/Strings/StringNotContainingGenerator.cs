using System;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class StringNotContainingGenerator : InlineGenerator<string>
  {
    private readonly string[] _excludedSubstrings;
    private readonly InlineGenerator<string> _stringGenerator;

    public StringNotContainingGenerator(string[] excludedSubstrings, InlineGenerator<string> stringGenerator)
    {
      _excludedSubstrings = excludedSubstrings;
      _stringGenerator = stringGenerator;
    }

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var preprocessedStrings = from str in _excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      var result = _stringGenerator.GenerateInstance(instanceGenerator, request);
      var found = false;
      for (int i = 0; i < 100; ++i)
      {
        result = _stringGenerator.GenerateInstance(instanceGenerator, request);
        if (!preprocessedStrings.Any(result.Contains))
        {
          found = true;
          break;
        }
      }

      if (!found)
      {
        foreach (var excludedSubstring in _excludedSubstrings.Where(s => s != string.Empty))
        {
          result = result.Replace(excludedSubstring, "");
        }
      }

      return result;
    }
  }
}