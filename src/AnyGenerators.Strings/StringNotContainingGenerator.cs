using System;
using System.Linq;
using TddXt.AnyExtensibility;

namespace Generators
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

    public string GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var preprocessedStrings = from str in _excludedSubstrings
        where !String.IsNullOrEmpty(str)
        select str;

      var result = _stringGenerator.GenerateInstance(instanceGenerator);
      var found = false;
      for (int i = 0; i < 100; ++i)
      {
        result = _stringGenerator.GenerateInstance(instanceGenerator);
        if (preprocessedStrings.Any(result.Contains))
        {
          found = true;
          break;
        }
      }

      if (!found)
      {
        foreach (var excludedSubstring in _excludedSubstrings.Where(s => s != String.Empty))
        {
          result = result.Replace(excludedSubstring, "");
        }
      }

      return result;
    }
  }
}