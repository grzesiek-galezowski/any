using Generators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class SimpleValueGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator gen) => gen.Value<T>();
  }

  public class SimpleInstanceGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator gen) => gen.Instance<T>();
  }
}