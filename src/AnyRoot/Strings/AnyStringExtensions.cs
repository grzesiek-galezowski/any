using System;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Strings;

public static class AnyStringExtensions
{
  extension(BasicGenerator gen)
  {
    public string String()
    {
      return gen.InstanceOf(InlineGenerators.String());
    }

    public string String(string seed)
    {
      return gen.InstanceOf(InlineGenerators.SeededString(seed));
    }

    public string LowerCaseString()
    {
      return gen.InstanceOf(InlineGenerators.LowercaseString());
    }

    public string UpperCaseString()
    {
      return gen.InstanceOf(InlineGenerators.UppercaseString());
    }

    public string LowerCaseAlphaString()
    {
      return gen.InstanceOf(InlineGenerators.LowercaseAlphaString());
    }

    public string UpperCaseAlphaString()
    {
      return gen.InstanceOf(InlineGenerators.UppercaseAlphaString());
    }

    public string StringMatching(string pattern)
    {
      return gen.InstanceOf(InlineGenerators.StringMatching(pattern));
    }

    public string String(int length)
    {
      return gen.InstanceOf(InlineGenerators.String(length));
    }

    [Obsolete("Use OtherThan() extension method instead")]
    public string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return gen.InstanceOf(InlineGenerators.ValueOtherThan(alreadyUsedStrings));
    }

    public string StringNotContaining<T>(params T[] excludedObjects)
    {
      return gen.InstanceOf(InlineGenerators.StringNotContaining(excludedObjects));
    }

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      return gen.InstanceOf(InlineGenerators.StringNotContaining(excludedSubstrings));
    }

    public string StringContaining<T>(T obj) where T : notnull
    {
      return gen.InstanceOf(InlineGenerators.StringContaining(obj.ToString().OrThrow()));
    }

    public string StringContaining(string str)
    {
      return gen.InstanceOf(InlineGenerators.StringContaining(str));
    }

    public string AlphaString()
    {
      return gen.InstanceOf(InlineGenerators.AlphaString(
        gen.InstanceOf(InlineGenerators.String()).Length));
    }

    public string AlphaString(int maxLength)
    {
      return gen.InstanceOf(InlineGenerators.AlphaString(maxLength));
    }

    public string Identifier()
    {
      return gen.InstanceOf(InlineGenerators.Identifier());
    }

    public char AlphaChar()
    {
      return gen.InstanceOf(InlineGenerators.AlphaChar());
    }

    public char DigitChar()
    {
      return gen.InstanceOf(InlineGenerators.DigitChar());
    }

    public char Char()
    {
      return gen.InstanceOf(InlineGenerators.Char());
    }

    public string NumericString(int digitsCount = Configuration.Many)
    {
      return gen.InstanceOf(InlineGenerators.NumericString(digitsCount));
    }

    public char LowerCaseAlphaChar()
    {
      return gen.InstanceOf(InlineGenerators.LowerCaseAlphaChar());
    }

    public char UpperCaseAlphaChar()
    {
      return gen.InstanceOf(InlineGenerators.UpperCaseAlphaChar());
    }
  }
}
