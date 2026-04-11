using AutoFixture;
using AutoFixture.Kernel;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

public class RegexGeneratorWrapper
{
  private readonly RegularExpressionGenerator _regexGenerator = new();

  public object Create(string pattern)
  {
    try
    {
      var request = new RegularExpressionRequest(pattern);
      return _regexGenerator.Create(request, new DummyContext());
    }
    catch (ObjectCreationException e)
    {
      throw new ThirdPartyGeneratorFailed(e);
    }
  }
}
