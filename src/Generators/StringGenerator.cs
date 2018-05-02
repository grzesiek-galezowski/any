using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using Generators;
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

    public string Ip(InstanceGenerator instanceGenerator)
    {
      return new IpStringGenerator().GenerateInstance(instanceGenerator);
    }

    public string String(InstanceGenerator instanceGenerator) => InlineGenerators.String().GenerateInstance(instanceGenerator);

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

    public static string String(ValueGenerator valueGenerator)
    {
      return valueGenerator.ValueOf<string>();
    }
  }

  
}