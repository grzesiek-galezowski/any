using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyAbstractType : IResolution
{
  public object? Apply(InstanceGenerator allGenerator, GenerationRequest generationRequest, Type type)
  {
    return default;
  }

  public bool AppliesTo(Type type)
  {
    return type.IsAbstract;
  }
}
