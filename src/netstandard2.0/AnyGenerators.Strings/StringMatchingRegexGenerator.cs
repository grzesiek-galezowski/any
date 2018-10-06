using TddXt.AnyExtensibility;
using TddXt.AutoFixtureWrapper;

namespace TddXt.AnyGenerators.Strings
{
  public class StringMatchingRegexGenerator : InlineGenerator<string>
  {
    private static readonly RegexGeneratorWrapper RegexGenerator = new RegexGeneratorWrapper();
    private readonly string _pattern;

    public StringMatchingRegexGenerator(string pattern)
    {
      _pattern = pattern;
    }

    public string GenerateInstance(InstanceGenerator gen, GenerationTrace trace)
    {

      var result = RegexGenerator.Create(_pattern);
      return result.ToString();
    }
  }
}