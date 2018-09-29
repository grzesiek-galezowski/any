using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Strings
{
  public class DigitCharGenerator : InlineGenerator<char>
  {
    private static readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    public char GenerateInstance(InstanceGenerator gen, GenerationTrace trace)
    {
      return _digitChars.Next();
    }
  }
}