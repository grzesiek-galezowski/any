using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic;

public class DummyGenerator<T> : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instanceGenerator.Dummy<T>(request);
  }
}