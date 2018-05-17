using AutoFixture;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class StringMatchingRegexGenerator : InlineGenerator<string>
  {
    private readonly string _pattern;
    private static readonly RegularExpressionGenerator RegexGenerator = new RegularExpressionGenerator();

    public StringMatchingRegexGenerator(string pattern)
    {
      _pattern = pattern;
    }

    public string GenerateInstance(InstanceGenerator gen)
    {
      var request = new RegularExpressionRequest(_pattern);

      var result = RegexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }
  }
}