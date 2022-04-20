using System;
using System.Threading;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Network;

public class IpStringGenerator : InlineGenerator<string>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return RandomGenerator.Value.Next(256) + "."
                                           + RandomGenerator.Value.Next(256) + "."
                                           + RandomGenerator.Value.Next(256) + "."
                                           + RandomGenerator.Value.Next(256);
  }
}