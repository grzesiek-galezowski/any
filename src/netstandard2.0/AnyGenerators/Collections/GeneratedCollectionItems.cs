using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  internal static class GeneratedCollectionItems
  {
    public static ICollection<T> AddTo<T>(ICollection<T> collection, int many,
      InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(instanceGenerator.Instance<T>(trace));
      }

      return collection;
    }
  }
}