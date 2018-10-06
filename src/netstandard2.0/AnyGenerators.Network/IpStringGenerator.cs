using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Network
{
  public class IpStringGenerator : InlineGenerator<string>
  {
    private static readonly Random RandomGenerator = new Random(Guid.NewGuid().GetHashCode());

    public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return RandomGenerator.Next(256) + "."
                                       + RandomGenerator.Next(256) + "."
                                       + RandomGenerator.Next(256) + "."
                                       + RandomGenerator.Next(256);
    }
  }
}