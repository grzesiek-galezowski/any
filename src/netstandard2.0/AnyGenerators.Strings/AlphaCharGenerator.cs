using CommonTypes;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Strings
{
  public class AlphaCharGenerator : InlineGenerator<char>
  {
    private static readonly CircularList<char> _letters =
      CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

    public char GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _letters.Next();
    }
  }
}