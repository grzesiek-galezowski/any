using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
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