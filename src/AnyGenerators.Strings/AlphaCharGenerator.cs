using TddEbook.TddToolkit.CommonTypes;
using TddXt.AnyExtensibility;

public class AlphaCharGenerator : InlineGenerator<char>
{
  private static readonly CircularList<char> _letters =
    CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

  public char GenerateInstance(InstanceGenerator instanceGenerator)
  {
    return _letters.Next();
  }
}