using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool AppliesTo(Type type);
    T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type);
  }
}
