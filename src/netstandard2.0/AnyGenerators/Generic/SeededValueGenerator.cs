using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic;

public class SeededValueGenerator<T> : InlineGenerator<T>
{
  private readonly T _seed;

  public SeededValueGenerator(T seed)
  {
    _seed = seed;
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instanceGenerator.Value(_seed, request);
  }
}