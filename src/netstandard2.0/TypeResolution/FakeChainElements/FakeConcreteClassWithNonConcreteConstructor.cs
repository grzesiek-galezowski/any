using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClassWithNonConcreteConstructor : IResolution
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;

    public FakeConcreteClassWithNonConcreteConstructor(FallbackTypeGenerator fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool AppliesTo(Type type)
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return _fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
    }
  }
}
