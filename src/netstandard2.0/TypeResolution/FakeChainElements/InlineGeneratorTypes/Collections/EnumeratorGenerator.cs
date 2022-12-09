using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class EnumeratorGenerator<T> : InlineGenerator<IEnumerator<T>>
{
  private readonly ManyStrategy _manyStrategy;

  public EnumeratorGenerator(ManyStrategy manyStrategy)
  {
    _manyStrategy = manyStrategy;
  }

  public IEnumerator<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return new EnumerableGenerator<T>(_manyStrategy)
      .AsList<T>().GenerateInstance(instanceGenerator, request).GetEnumerator();
  }
}
