using System;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Strings;

public static class AnyStringExtensions
{
  extension(Any)
  {
    public static string String()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.String());
    }

    public static string String(string seed)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.SeededString(seed));
    }

    public static string LowerCaseString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.LowercaseString());
    }

    public static string UpperCaseString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UppercaseString());
    }

    public static string LowerCaseAlphaString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.LowercaseAlphaString());
    }

    public static string UpperCaseAlphaString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UppercaseAlphaString());
    }

    public static string StringMatching(string pattern)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StringMatching(pattern));
    }

    public static string String(int length)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.String(length));
    }

    [Obsolete("Use OtherThan() extension method instead")]
    public static string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.ValueOtherThan(alreadyUsedStrings));
    }

    public static string StringNotContaining<T>(params T[] excludedObjects)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StringNotContaining(excludedObjects));
    }

    public static string StringNotContaining(params string[] excludedSubstrings)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StringNotContaining(excludedSubstrings));
    }

    public static string StringContaining<T>(T obj) where T : notnull
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StringContaining(obj.ToString().OrThrow()));
    }

    public static string StringContaining(string str)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StringContaining(str));
    }

    public static string AlphaString()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.AlphaString(
        Any.InstanceOf(InlineGenerators.InlineGenerators.String()).Length));
    }

    public static string AlphaString(int maxLength)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.AlphaString(maxLength));
    }

    public static string Identifier()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Identifier());
    }

    public static char AlphaChar()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.AlphaChar());
    }

    public static char DigitChar()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.DigitChar());
    }

    public static char Char()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Char());
    }

    public static string NumericString(int digitsCount = Configuration.Many)
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.NumericString(digitsCount));
    }

    public static char LowerCaseAlphaChar()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.LowerCaseAlphaChar());
    }

    public static char UpperCaseAlphaChar()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.UpperCaseAlphaChar());
    }
  }
}
