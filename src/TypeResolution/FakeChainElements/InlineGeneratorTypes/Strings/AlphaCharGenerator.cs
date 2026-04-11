using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class AlphaCharGenerator : InlineGenerator<char>
{
  private static readonly CircularList<char> _letters =
    CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

  public char GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _letters.Next();
  }
}
