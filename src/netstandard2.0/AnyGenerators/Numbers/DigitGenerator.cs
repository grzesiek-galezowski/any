using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Numbers
{
  public class DigitGenerator : InlineGenerator<byte>
  {
    private static readonly CircularList<byte> Digits =
      CircularList.CreateStartingFromRandom(new byte[] {5, 6, 4, 7, 3, 8, 2, 9, 1, 0});

    public byte GenerateInstance(InstanceGenerator gen, GenerationTrace trace) => Digits.Next();
  }
}