using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IGenerationChain
  {
    object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type);
  }
}
