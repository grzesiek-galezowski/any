using System;
using AutoFixtureWrapper;
using TddXt.AnyExtensibility;

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

    public string GenerateInstance(InstanceGenerator gen)
    {

      var result = RegexGenerator.Create(_pattern);
      return result.ToString();
    }
  }
}