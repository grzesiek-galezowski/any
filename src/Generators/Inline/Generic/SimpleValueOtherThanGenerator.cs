using Generators;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.Generators
{
  public class SimpleValueOtherThanGenerator<T> : InlineGenerator<T>
  {
    private readonly T[] _excluded;

    public SimpleValueOtherThanGenerator(T[] excluded) => _excluded = excluded;
    public T GenerateInstance(InstanceGenerator gen) => gen.ValueOtherThan(_excluded);
  }
}