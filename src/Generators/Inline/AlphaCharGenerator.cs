using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

public class AlphaCharGenerator : InlineGenerator<char>
{
  private readonly CircularList<char> _letters =
    CircularList.CreateStartingFromRandom("qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());

  public char GenerateInstance(InstanceGenerator instanceGenerator)
  {
    return _letters.Next();
  }
}