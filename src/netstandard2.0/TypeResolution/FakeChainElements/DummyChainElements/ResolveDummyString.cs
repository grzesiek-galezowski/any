using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyString : IResolution
{
  public object Apply(InstanceGenerator allGenerator, GenerationRequest request, Type type)
  {
    return allGenerator.Instance(type, request.DisableNestingLimit());
  }

  public bool AppliesTo(Type type)
  {
    return type == typeof(string);
  }
}
