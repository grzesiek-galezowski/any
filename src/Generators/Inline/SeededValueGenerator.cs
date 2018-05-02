using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class SeededValueGenerator<T> : InlineGenerator<T>
  {
    private readonly T _seed;

    public SeededValueGenerator(T seed)
    {
      _seed = seed;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.ValueOf(_seed);
    }
  }
}