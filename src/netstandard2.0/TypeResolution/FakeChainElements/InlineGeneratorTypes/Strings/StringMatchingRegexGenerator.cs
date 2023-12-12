using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class StringMatchingRegexGenerator(string pattern) : InlineGenerator<string>
{
  private static readonly RegexGeneratorWrapper RegexGenerator = new RegexGeneratorWrapper();

  public string GenerateInstance(InstanceGenerator gen, GenerationRequest request)
  {
    var result = RegexGenerator.Create(pattern);
    return result.ToString().OrThrow();
  }
}
