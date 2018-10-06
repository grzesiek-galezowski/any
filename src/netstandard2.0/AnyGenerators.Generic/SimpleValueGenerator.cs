using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
{
  public class SimpleValueGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator gen, GenerationTrace trace) => gen.Value<T>(trace);
  }

  public class SimpleInstanceGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator gen, GenerationTrace trace) => gen.Instance<T>(trace);
  }
}