using System;
using TddXt.AnyExtensibility;
using TddXt.AutoFixtureWrapper;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Strings
{
  public class StringMatchingRegexGenerator : InlineGenerator<string>
  {
    private readonly string _pattern;
    private static readonly RegexGeneratorWrapper RegexGenerator = new RegexGeneratorWrapper();

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