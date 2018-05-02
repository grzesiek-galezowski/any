using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class StringExtensions
  {
    public static string String(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.String());
    }

    public static string String(this MyGenerator gen, string seed)
    {
      return gen.InstanceOf(InlineGenerators.SeededString(seed));
    }

    public static string LowerCaseString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowercaseString());
    }

    public static string UpperCaseString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UppercaseString());
    }

    public static string LowerCaseAlphaString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowercaseAlphaString());
    }

    public static string UpperCaseAlphaString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UppercaseAlphaString());
    }

    public static string StringMatching(this MyGenerator gen, string pattern)
    {
      return gen.InstanceOf(InlineGenerators.StringMatching(pattern));
    }

    public static string String(this MyGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.String(length));
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
      return gen.InstanceOf(InlineGenerators.AlphaString(
          gen.InstanceOf(InlineGenerators.String()).Length));
    }

    public static string AlphaString(this MyGenerator gen, int maxLength)
    {
      return gen.InstanceOf(InlineGenerators.AlphaString(maxLength));
    }

    public static string Identifier(this MyGenerator gen)
    {
      return gen.AllGenerator.Identifier();
    }

    public static char AlphaChar(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.AlphaChar());
    }

    public static char DigitChar(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.DigitChar());
    }

    public static char Char(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Char());
    }

    public static string NumericString(this MyGenerator gen, int digitsCount = AllGenerator.Many)
    {
      //todo gen for Many could be cached
      return InlineGenerators.NumericString(digitsCount).GenerateInstance(gen.AllGenerator);
    }

    public static char LowerCaseAlphaChar(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowerCaseAlphaChar());
    }

    public static char UpperCaseAlphaChar(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UpperCaseAlphaChar());
    }
  }
}