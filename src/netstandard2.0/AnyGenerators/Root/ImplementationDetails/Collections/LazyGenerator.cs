using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Collections;

public class LazyGenerator<T> : InlineGenerator<Lazy<T>>
{
  public Lazy<T> GenerateInstance(
    InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return new Lazy<T>(() => instanceGenerator.Instance<T>(request));
  }
}
