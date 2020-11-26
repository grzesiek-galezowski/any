using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
{
  public class SimpleValueOtherThanGenerator<T> : InlineGenerator<T>
  {
    private readonly T[] _excluded;

    public SimpleValueOtherThanGenerator(T[] excluded)
    {
      _excluded = excluded;
    }

    public T GenerateInstance(InstanceGenerator gen, GenerationTrace trace) => gen.ValueOtherThan(_excluded);
  }
}