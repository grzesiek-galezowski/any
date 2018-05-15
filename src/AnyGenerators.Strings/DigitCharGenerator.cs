using TddEbook.TddToolkit.CommonTypes;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.Generators
{
  public class DigitCharGenerator : InlineGenerator<char>
  {
    private static readonly CircularList<char> _digitChars =
      CircularList.CreateStartingFromRandom("5647382910".ToCharArray());

    public char GenerateInstance(InstanceGenerator gen)
    {
      return _digitChars.Next();
    }
  }
}