using System;
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
}