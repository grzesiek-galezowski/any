using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyPrimitiveTypeInstance : IResolution
{
  public object Apply(
    InstanceGenerator allGenerator, 
    GenerationRequest request, 
    Type type)
  {
    return allGenerator.Instance(type, request.DisableLimits());
  }

  public bool AppliesTo(Type type)
  {
    return type.IsPrimitive;
  }
}
