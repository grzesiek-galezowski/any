using System;
using System.Threading;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Network;

public class PortNumberGenerator : InlineGenerator<int>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return RandomGenerator.Value.Next(0, 65535);
  }
}