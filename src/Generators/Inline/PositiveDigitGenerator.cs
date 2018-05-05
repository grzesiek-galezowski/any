using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class PositiveDigitGenerator : InlineGenerator<byte>
  {
    private readonly InlineGenerator<byte> _digitGenerator;

    public PositiveDigitGenerator(InlineGenerator<byte> digitGenerator)
    {
      _digitGenerator = digitGenerator;
    }

    public byte GenerateInstance(InstanceGenerator instanceGenerator)
    {
      byte digit = _digitGenerator.GenerateInstance(instanceGenerator);
      while (digit == 0)
      {
        digit = _digitGenerator.GenerateInstance(instanceGenerator);
      }

      return digit;
    }
  }
}