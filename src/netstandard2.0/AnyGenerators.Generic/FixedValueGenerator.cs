using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
{
  public class FixedValueGenerator<T> : InlineGenerator<T>
  {
    private readonly T _instance;

    public FixedValueGenerator(T instance)
    {
      _instance = instance;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _instance;
    }
  }
}