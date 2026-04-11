using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Numbers;

public class PositiveDigitGenerator(InlineGenerator<byte> digitGenerator) : InlineGenerator<byte>
{
  public byte GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    byte digit = digitGenerator.GenerateInstance(instanceGenerator, request);
    while (digit == 0)
    {
      digit = digitGenerator.GenerateInstance(instanceGenerator, request);
    }

    return digit;
  }
}
