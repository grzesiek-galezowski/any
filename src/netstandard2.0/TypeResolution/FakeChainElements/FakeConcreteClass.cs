using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeConcreteClass : IResolution
{
  private readonly FallbackTypeGenerator _fallbackTypeGenerator;

  public FakeConcreteClass(FallbackTypeGenerator fallbackTypeGenerator)
  {
    _fallbackTypeGenerator = fallbackTypeGenerator;
  }

  public bool AppliesTo(Type type)
  {
    return true;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request, type);
  }
}
