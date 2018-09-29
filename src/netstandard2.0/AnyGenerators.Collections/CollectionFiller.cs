using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Collections
{
  internal static class CollectionFiller //todo make up a better name
  {
    public static ICollection<T> FillingCollection<T>(ICollection<T> collection, int many,
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