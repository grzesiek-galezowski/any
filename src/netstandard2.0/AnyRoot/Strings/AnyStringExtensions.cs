using System;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Strings
{
  public static class AnyStringExtensions
  {
    public static string String(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.String());
    }

    public static string String(this BasicGenerator gen, string seed)
    {
      return gen.InstanceOf(InlineGenerators.SeededString(seed));
    }

    public static string LowerCaseString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowercaseString());
    }

    public static string UpperCaseString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UppercaseString());
    }

    public static string LowerCaseAlphaString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowercaseAlphaString());
    }

    public static string UpperCaseAlphaString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UppercaseAlphaString());
    }

    public static string StringMatching(this BasicGenerator gen, string pattern)
    {
      return gen.InstanceOf(InlineGenerators.StringMatching(pattern));
    }

    public static string String(this BasicGenerator gen, int length)
    {
      return gen.InstanceOf(InlineGenerators.String(length));
    }

    [Obsolete("Use OtherThan() extension method instead")]
    public static string StringOtherThan(this BasicGenerator gen, params string[] alreadyUsedStrings)
    {
      return gen.InstanceOf(InlineGenerators.ValueOtherThan(alreadyUsedStrings));
    }

    public static string StringNotContaining<T>(this BasicGenerator gen, params T[] excludedObjects)
    {
      return gen.InstanceOf(InlineGenerators.StringNotContaining(excludedObjects));
    }

    public static string StringNotContaining(this BasicGenerator gen, params string[] excludedSubstrings)
    {
      return gen.InstanceOf(InlineGenerators.StringNotContaining(excludedSubstrings));
    }

    public static string StringContaining<T>(this BasicGenerator gen, T obj)
    {
      return gen.InstanceOf(InlineGenerators.StringContaining(obj.ToString()));
    }

    public static string StringContaining(this BasicGenerator gen, string str)
    {
      return gen.InstanceOf(InlineGenerators.StringContaining(str));
    }

    public static string AlphaString(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.AlphaString(
          gen.InstanceOf(InlineGenerators.String()).Length));
    }

    public static string AlphaString(this BasicGenerator gen, int maxLength)
    {
      return gen.InstanceOf(InlineGenerators.AlphaString(maxLength));
    }

    public static string Identifier(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Identifier());
    }

    public static char AlphaChar(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.AlphaChar());
    }

    public static char DigitChar(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.DigitChar());
    }

    public static char Char(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Char());
    }

    public static string NumericString(this BasicGenerator gen, int digitsCount = Configuration.Many)
    {
      return gen.InstanceOf(InlineGenerators.NumericString(digitsCount));
    }

    public static char LowerCaseAlphaChar(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.LowerCaseAlphaChar());
    }

    public static char UpperCaseAlphaChar(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UpperCaseAlphaChar());
    }
  }
}