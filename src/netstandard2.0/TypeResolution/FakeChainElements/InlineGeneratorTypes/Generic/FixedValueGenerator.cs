using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class FixedValueGenerator<T> : InlineGenerator<T>
{
  private readonly T _instance;

  public FixedValueGenerator(T instance)
  {
    _instance = instance;
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return _instance;
  }
}
