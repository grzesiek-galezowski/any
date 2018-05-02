using System;
using Generators;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

internal class LowercaseAlphaStringGenerator : InlineGenerator<string>
{
  private readonly ValueConversion<string, string> _valueConversion;

  public LowercaseAlphaStringGenerator()
  {
    _valueConversion = new ValueConversion<string, string>(
      InlineGenerators.AlphaString(Guid.NewGuid().ToString().Length), s => s.ToLowerInvariant());
  }

  public string GenerateInstance(InstanceGenerator instanceGenerator)
  {
    var valueConversion = _valueConversion;
    return valueConversion.GenerateInstance(instanceGenerator);
  }
}