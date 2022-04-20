using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic;

public class SimpleValueGenerator<T> : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.Value<T>(request);
}

public class SimpleInstanceGenerator<T> : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.Instance<T>(request);
}