using System;

namespace TddEbook.TddToolkit.Generators
{
  public class SpecificTypeObjectGenerator
  {
    private readonly ValueGenerator _valueGenerator;

    public SpecificTypeObjectGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public Uri Uri()
    {
      return _valueGenerator.ValueOf<Uri>();
    }

    public Guid Guid()
    {
      return _valueGenerator.ValueOf<Guid>();
    }
  }
}