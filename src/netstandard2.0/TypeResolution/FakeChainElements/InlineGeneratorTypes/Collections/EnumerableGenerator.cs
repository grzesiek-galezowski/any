using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class EnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
{
  private readonly ManyStrategy _manyStrategy;

  public EnumerableGenerator(ManyStrategy manyStrategy)
  {
    _manyStrategy = manyStrategy;
  }

  public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    //todo create empty collection factory to be able to use object here
    return GeneratedCollectionItems.AddTo(new List<T>(), _manyStrategy.GetMany(request), instanceGenerator, request);
  }
}

