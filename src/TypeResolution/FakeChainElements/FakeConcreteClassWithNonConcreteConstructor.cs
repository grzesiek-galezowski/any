using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public class FakeConcreteClassWithNonConcreteConstructor<T> : IResolution<T>
  {
    readonly FallbackTypeGenerator<T> _fallbackTypeGenerator = new FallbackTypeGenerator<T>();

    public bool Applies()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }

    public T Apply(InstanceGenerator instanceGenerator)
    {
      return _fallbackTypeGenerator.GenerateInstance(instanceGenerator);
    }
  }
}