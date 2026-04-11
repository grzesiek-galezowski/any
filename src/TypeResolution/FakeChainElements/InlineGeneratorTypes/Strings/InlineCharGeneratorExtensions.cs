using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

public static class InlineCharGeneratorExtensions
{
  public static ValueConversion<char, char> AsUpperCase(this InlineGenerator<char> gen)
  {
    return Conversion(gen, char.ToUpperInvariant);
  }

  public static ValueConversion<char, char> AsLowerCase(this InlineGenerator<char> gen)
  {
    return Conversion(gen, char.ToLowerInvariant);
  }

  private static ValueConversion<T, U> Conversion<T, U>(
    InlineGenerator<T> gen,
    Func<T, U> conversion)
  {
    return new ValueConversion<T, U>(gen, conversion);
  }
}
