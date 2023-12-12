using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class StringNotContainingGenerator(string[] excludedSubstrings, InlineGenerator<string> stringGenerator)
  : InlineGenerator<string>
{
  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var preprocessedStrings = from str in excludedSubstrings
      where !string.IsNullOrEmpty(str)
      select str;

    var result = stringGenerator.GenerateInstance(instanceGenerator, request);
    var found = false;
    for (int i = 0; i < 100; ++i)
    {
      result = stringGenerator.GenerateInstance(instanceGenerator, request);
      if (!preprocessedStrings.Any(result.Contains))
      {
        found = true;
        break;
      }
    }

    if (!found)
    {
      foreach (var excludedSubstring in excludedSubstrings.Where(s => s != string.Empty))
      {
        result = result.Replace(excludedSubstring, "");
      }
    }

    return result;
  }
}
