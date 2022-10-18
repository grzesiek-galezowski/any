using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Generic;

public class SimpleInstanceOtherThanGenerator<T> : InlineGenerator<T>
{
  private readonly T[] _excluded;

  public SimpleInstanceOtherThanGenerator(T[] excluded)
  {
    _excluded = excluded;
  }

  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => gen.OtherThan(_excluded);
}