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

    public bool Applies()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _fallbackTypeGenerator.GenerateInstance(instanceGenerator, trace);
    }
  }
}