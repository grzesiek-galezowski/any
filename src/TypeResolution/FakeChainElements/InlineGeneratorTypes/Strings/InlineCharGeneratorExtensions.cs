using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public static class InlineCharGeneratorExtensions
{
  extension(InlineGenerator<char> gen)
  {
    public ValueConversion<char, char> AsUpperCase()
    {
      return Conversion(gen, char.ToUpperInvariant);
    }

    public ValueConversion<char, char> AsLowerCase()
    {
      return Conversion(gen, char.ToLowerInvariant);
    }
  }

  private static ValueConversion<T, U> Conversion<T, U>(
    InlineGenerator<T> gen,
    Func<T, U> conversion)
  {
    return new ValueConversion<T, U>(gen, conversion);
  }
}
