#if NETFRAMEWORK
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture;
#else
using AutoFixture;
using AutoFixture.Kernel;
#endif

using TddXt.CommonTypes;

namespace TddXt.AutoFixtureWrapper
{
    public class RegexGeneratorWrapper
    {
      private readonly RegularExpressionGenerator _regexGenerator = new RegularExpressionGenerator();

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
}