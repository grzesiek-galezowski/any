using System;
using System.Threading;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Network;

public class IpStringGenerator : InlineGenerator<string>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public string GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var randomGen = RandomGenerator.Value.OrThrow();
    return randomGen
        .Next(256) 
           + "." 
           + randomGen.Next(256) 
           + "."
           + randomGen.Next(256) 
           + "."
           + randomGen.Next(256);
  }
}
