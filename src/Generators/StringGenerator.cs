using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class IpStringGenerator : InlineGenerator<string>
  {
    private static readonly Random RandomGenerator = new Random(Guid.NewGuid().GetHashCode());

    public string GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return RandomGenerator.Next(256) + "."
                                        + RandomGenerator.Next(256) + "."
                                        + RandomGenerator.Next(256) + "."
                                        + RandomGenerator.Next(256);
    }
  }

  public class StringMatchingRegexGenerator : InlineGenerator<string>
  {
    private readonly string _pattern;
    private static readonly RegularExpressionGenerator RegexGenerator = new RegularExpressionGenerator();

    public StringMatchingRegexGenerator(string pattern)
    {
      _pattern = pattern;
    }

    public string GenerateInstance(InstanceGenerator gen)
    {
      var request = new RegularExpressionRequest(_pattern);

      var result = RegexGenerator.Create(request, new DummyContext());
      return result.ToString();
    }
  }

  public class StringGenerator
  {
    private readonly ValueGenerator _valueGenerator;

    public StringGenerator(ValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public string LegalXmlTagName(InstanceGenerator instanceGenerator)
    {
      return Identifier(instanceGenerator);
    }

    public string UrlString()
    {
      return _valueGenerator.ValueOf<Uri>().ToString();
    }

    public string Ip(InstanceGenerator instanceGenerator)
    {
      return new IpStringGenerator().GenerateInstance(instanceGenerator);
    }

    public string String(InstanceGenerator instanceGenerator) => new SimpleValueGenerator<string>().GenerateInstance(instanceGenerator);
    public string String(string seed, InstanceGenerator instanceGenerator) => _valueGenerator.ValueOf(seed + "_");

    public string LowerCaseString(InstanceGenerator instanceGenerator) => String(instanceGenerator).ToLower();
    public string UpperCaseString(InstanceGenerator instanceGenerator) => String(instanceGenerator).ToUpper();
    public string LowerCaseAlphaString(InstanceGenerator instanceGenerator) => AlphaString(instanceGenerator).ToLower();
    public string UpperCaseAlphaString(InstanceGenerator instanceGenerator) => AlphaString(instanceGenerator).ToUpper();

    public string StringMatching(string pattern, InstanceGenerator instanceGenerator)
    {
      return new StringMatchingRegexGenerator(pattern).GenerateInstance(instanceGenerator);
    }

    public string String(int charactersCount, InstanceGenerator instanceGenerator)
    {
      var result = string.Empty;
      while (result.Length < charactersCount)
      {
        result += String(instanceGenerator);
      }
      return result.Substring(0, charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings) 
      => _valueGenerator.ValueOtherThan(alreadyUsedStrings);

    public string StringNotContaining<T>(InstanceGenerator instanceGenerator, params T[] excludedObjects) => 
      StringNotContaining(instanceGenerator, (from obj in excludedObjects select obj.ToString()).ToArray());

    public string StringNotContaining(InstanceGenerator instanceGenerator, params string[] excludedSubstrings)
    {
      var preprocessedStrings = from str in excludedSubstrings
        where !string.IsNullOrEmpty(str)
        select str;

      var result = String(instanceGenerator);
      var found = false;
      for(int i = 0 ; i < 100 ; ++i)
      {
        result = String(instanceGenerator);
        if (preprocessedStrings.Any(result.Contains))
        {
          found = true;
          break;
        }
      }
      if (!found)
      {
        foreach (var excludedSubstring in excludedSubstrings.Where(s => s != string.Empty))
        {
          result = result.Replace(excludedSubstring, "");
        }
      }
      return result;
    }

    public string StringContaining<T>(T obj, InstanceGenerator instanceGenerator) => 
      StringContaining(obj.ToString(), instanceGenerator);

    public string StringContaining(string str, InstanceGenerator instanceGenerator) => 
      String(instanceGenerator) + str + String(instanceGenerator);

    public string AlphaString(InstanceGenerator instanceGenerator) => 
      AlphaString(String(instanceGenerator).Length, instanceGenerator);

    public string AlphaString(int maxLength, InstanceGenerator instanceGenerator)
    {
      var result = string.Empty;
      for (var i = 0; i < maxLength; ++i)
      {
        result += AlphaChar(instanceGenerator);
      }
      return result;
    }

    public string Identifier(InstanceGenerator instanceGenerator)
    {
      string result = AlphaChar(instanceGenerator).ToString(CultureInfo.InvariantCulture);
      for (var i = 0; i < 5; ++i)
      {
        result += DigitChar(instanceGenerator);
        result += AlphaChar(instanceGenerator);
      }
      return result;
    }

    private char AlphaChar(InstanceGenerator instanceGenerator)
    {
      return InlineGenerators.AlphaChar().GenerateInstance(instanceGenerator);
    }

    private char DigitChar(InstanceGenerator instanceGenerator)
    {
      return InlineGenerators.DigitChar().GenerateInstance(instanceGenerator);
    }

    public static string String(ValueGenerator valueGenerator)
    {
      return valueGenerator.ValueOf<string>();
    }
  }

  
}