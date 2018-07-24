using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Network
{
  public class PortNumberGenerator : InlineGenerator<int>
  {
    private static readonly Random RandomGenerator = new Random(System.Guid.NewGuid().GetHashCode());

    public int GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return RandomGenerator.Next(0, 65535);
    }
  }
}