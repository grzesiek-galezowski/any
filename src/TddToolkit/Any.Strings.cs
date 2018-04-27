using TddEbook.TddToolkit.Generators;

namespace TddEbook.TddToolkit
{

  public partial class Any
  {
    public static string String()
    {
      return gen.String();
    }

    public static string String(string seed)
    {
      return gen.String(seed);
    }

    public static string LowerCaseString()
    {
      return gen.LowerCaseString();
    }

    public static string UpperCaseString()
    {
      return gen.UpperCaseString();
    }

    public static string LowerCaseAlphaString()
    {
      return gen.LowerCaseAlphaString();
    }

    public static string UpperCaseAlphaString()
    {
      return gen.UpperCaseAlphaString();
    }


    public static string StringMatching(string pattern)
    {
      return gen.StringMatching(pattern);
    }

    public static string StringOfLength(int charactersCount)
    {
      return gen.StringOfLength(charactersCount);
    }

    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return gen.StringOtherThan(alreadyUsedStrings);
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
      return gen.StringNotContaining(excludedObjects);
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      return gen.StringNotContaining(excludedSubstrings);
    }

    public static string StringContaining<T>(T obj)
    {
      return gen.StringContaining(obj);
    }

    public static string StringContaining(string str)
    {
      return gen.StringContaining(str);
    }

    public static string AlphaString()
    {
      return gen.AlphaString();
    }

    public static string AlphaString(int maxLength)
    {
      return gen.AlphaString(maxLength);
    }

    public static string Identifier()
    {
      return gen.Identifier();
    }

    public static char AlphaChar()
    {
      return gen.AlphaChar();
    }

    public static char DigitChar()
    {
      return gen.DigitChar();
    }

    public static char Char()
    {
      return gen.Char();
    }

    public static string NumericString(int digitsCount = AllGenerator.Many)
    {
      return gen.NumericString(digitsCount);
    }

    public static char LowerCaseAlphaChar()
    {
      return gen.LowerCaseAlphaChar();
    }

    public static char UpperCaseAlphaChar()
    {
      return gen.UpperCaseAlphaChar();
    }
  }
}
