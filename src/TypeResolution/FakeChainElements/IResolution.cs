using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public interface IResolution
{
  bool AppliesTo(Type type);
  object? Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type);
}
