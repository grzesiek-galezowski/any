using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class EnumerableGenerator<T>(ManyStrategy manyStrategy) : InlineGenerator<IEnumerable<T>>
{
  public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    //todo create empty collection factory to be able to use object here
    return GeneratedCollectionItems.AddTo(new List<T>(), manyStrategy.GetMany(request), instanceGenerator, request);
  }
}

