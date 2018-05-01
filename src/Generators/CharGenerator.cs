using Generators;

namespace TddEbook.TddToolkit.Generators
{
  public class CharGenerator
  {
    private readonly ValueGenerator _valueGenerator;

    public CharGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public char Char() => _valueGenerator.ValueOf<char>();
  }
}