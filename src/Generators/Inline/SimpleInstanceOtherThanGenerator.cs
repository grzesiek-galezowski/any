using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class SimpleInstanceOtherThanGenerator<T> : InlineGenerator<T>
  {
    private readonly T[] _excluded;

    public SimpleInstanceOtherThanGenerator(T[] excluded) => _excluded = excluded;
    public T GenerateInstance(InstanceGenerator gen) => gen.OtherThan(_excluded);
  }
}