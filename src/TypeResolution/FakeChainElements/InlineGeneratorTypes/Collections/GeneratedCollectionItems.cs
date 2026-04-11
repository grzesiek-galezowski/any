using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

internal static class GeneratedCollectionItems
{
  public static ICollection<T> AddTo<T>(ICollection<T> collection, int many,
    InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    for (int i = 0; i < many; ++i)
    {
      collection.Add(instanceGenerator.Instance<T>(request));
    }

    return collection;
  }
}
