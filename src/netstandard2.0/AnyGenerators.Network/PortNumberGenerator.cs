using System;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Network
{
  public class PortNumberGenerator : InlineGenerator<int>
  {
    private static readonly Random RandomGenerator = new Random(System.Guid.NewGuid().GetHashCode());

    public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return RandomGenerator.Next(0, 65535);
    }
  }
}