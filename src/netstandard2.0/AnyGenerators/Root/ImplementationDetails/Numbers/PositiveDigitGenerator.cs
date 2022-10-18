using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Numbers;

public class PositiveDigitGenerator : InlineGenerator<byte>
{
  private readonly InlineGenerator<byte> _digitGenerator;

  public PositiveDigitGenerator(InlineGenerator<byte> digitGenerator)
  {
    _digitGenerator = digitGenerator;
  }

  public byte GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    byte digit = _digitGenerator.GenerateInstance(instanceGenerator, request);
    while (digit == 0)
    {
      digit = _digitGenerator.GenerateInstance(instanceGenerator, request);
    }

    return digit;
  }
}