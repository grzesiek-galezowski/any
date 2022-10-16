using System;
using System.Runtime.Serialization;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class FallbackDummyObjectResolution : IResolution
{
  public object? Apply(InstanceGenerator allGenerator, GenerationRequest generationRequest, Type type)
  {
    return FormatterServices.GetUninitializedObject(type);
  }

  public bool AppliesTo(Type type)
  {
    return true;
  }
}
