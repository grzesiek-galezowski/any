using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public class DigitCharGenerator : InlineGenerator<char>
{
  private static readonly CircularList<char> _digitChars =
    CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

  public char GenerateInstance(InstanceGenerator gen, GenerationRequest request)
  {
    return _digitChars.Next();
  }
}
