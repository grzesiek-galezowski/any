using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class EnumeratorGenerator<T> : InlineGenerator<IEnumerator<T>>
{
  public IEnumerator<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return new EnumerableGenerator<T>(Configuration.Many)
      .AsList<T>().GenerateInstance(instanceGenerator, request).GetEnumerator();
  }
}
