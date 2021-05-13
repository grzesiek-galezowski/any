using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    private readonly FallbackTypeGenerator<T> _fallbackTypeGenerator;

    public FakeConcreteClassWithNonConcreteConstructor(FallbackTypeGenerator<T> fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public bool AppliesTo(Type type)
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, request);
    }
  }
}
