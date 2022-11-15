﻿using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class StringMatchingRegexGenerator : InlineGenerator<string>
{
  private static readonly RegexGeneratorWrapper RegexGenerator = new RegexGeneratorWrapper();
  private readonly string _pattern;

  public StringMatchingRegexGenerator(string pattern)
  {
    _pattern = pattern;
  }

  public string GenerateInstance(InstanceGenerator gen, GenerationRequest request)
  {
    var result = RegexGenerator.Create(_pattern);
    return result.ToString().OrThrow();
  }
}
