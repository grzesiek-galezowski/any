using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace AnyCore
{
  public static class StringExtensions
  {
    public static string String(this MyGenerator gen)
    {
      return gen.AllGenerator.String();
    }

    public static string String(this MyGenerator gen, string seed)
    {
      return gen.AllGenerator.String(seed);
    }

    public static string LowerCaseString(this MyGenerator gen)
    {
      return gen.AllGenerator.LowerCaseString();
    }

    public static string UpperCaseString(this MyGenerator gen)
    {
      return gen.AllGenerator.UpperCaseString();
    }

    public static string LowerCaseAlphaString(this MyGenerator gen)
    {
      return gen.AllGenerator.LowerCaseAlphaString();
    }

    public static string UpperCaseAlphaString(this MyGenerator gen)
    {
      return gen.AllGenerator.UpperCaseAlphaString();
    }


    public static string StringMatching(this MyGenerator gen, string pattern)
    {
      return gen.AllGenerator.StringMatching(pattern);
    }

    public static string StringOfLength(this MyGenerator gen, int charactersCount)
    {
      return gen.AllGenerator.StringOfLength(charactersCount);
    }

    public static string StringOtherThan(this MyGenerator gen, params string[] alreadyUsedStrings)
    {
      return gen.AllGenerator.StringOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining<T>(this MyGenerator gen, params T[] excludedObjects)
    {
      return gen.AllGenerator.StringNotContaining(excludedObjects);
    }

    public static string StringNotContaining(this MyGenerator gen, params string[] excludedSubstrings)
    {
      return gen.AllGenerator.StringNotContaining(excludedSubstrings);
    }

    public static string StringContaining<T>(this MyGenerator gen, T obj)
    {
      return gen.AllGenerator.StringContaining(obj);
    }

    public static string StringContaining(this MyGenerator gen, string str)
    {
      return gen.AllGenerator.StringContaining(str);
    }

    public static string AlphaString(this MyGenerator gen)
    {
      return gen.AllGenerator.AlphaString();
    }

    public static string AlphaString(this MyGenerator gen, int maxLength)
    {
      return gen.AllGenerator.AlphaString(maxLength);
    }

    public static string Identifier(this MyGenerator gen)
    {
      return gen.AllGenerator.Identifier();
    }

    public static char AlphaChar(this MyGenerator gen)
    {
      return gen.AllGenerator.AlphaChar();
    }

    public static char DigitChar(this MyGenerator gen)
    {
      return gen.AllGenerator.DigitChar();
    }

    public static char Char(this MyGenerator gen)
    {
      return gen.AllGenerator.Char();
    }

    public static string NumericString(this MyGenerator gen, int digitsCount = AllGenerator.Many)
    {
      return gen.AllGenerator.NumericString(digitsCount);
    }

    public static char LowerCaseAlphaChar(this MyGenerator gen)
    {
      return gen.AllGenerator.LowerCaseAlphaChar();
    }

    public static char UpperCaseAlphaChar(this MyGenerator gen)
    {
      return gen.AllGenerator.UpperCaseAlphaChar();
    }
  }
}