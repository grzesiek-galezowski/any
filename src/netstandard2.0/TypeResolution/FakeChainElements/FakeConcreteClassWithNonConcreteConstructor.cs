using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
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

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return (T)_fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
    }
  }
}
