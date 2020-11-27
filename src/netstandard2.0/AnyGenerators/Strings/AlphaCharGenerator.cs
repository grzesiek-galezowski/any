using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.CommonTypes;

namespace TddXt.AnyGenerators.Strings
{
  public class AlphaCharGenerator : InlineGenerator<char>
  {
    private static readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    public char GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _letters.Next();
    }
  }
}