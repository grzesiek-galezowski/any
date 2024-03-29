﻿using System;
using System.Threading;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Network;

public class PortNumberGenerator : InlineGenerator<int>
{
  private static readonly ThreadLocal<Random> RandomGenerator = new(() => new Random(Guid.NewGuid().GetHashCode()));

  public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return RandomGenerator.Value.OrThrow().Next(0, 65535);
  }
}
