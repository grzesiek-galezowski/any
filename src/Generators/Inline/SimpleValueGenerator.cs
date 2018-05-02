using Generators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class SimpleValueGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator gen) => gen.ValueOf<T>();
  }
}