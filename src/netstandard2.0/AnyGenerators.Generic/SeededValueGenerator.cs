using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
{
  public class SeededValueGenerator<T> : InlineGenerator<T>
  {
    private readonly T _seed;

    public SeededValueGenerator(T seed)
    {
      _seed = seed;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return instanceGenerator.Value(_seed, trace);
    }
  }
}