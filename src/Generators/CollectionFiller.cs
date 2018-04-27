using System.Collections.Generic;
using TddEbook.TypeReflection;

namespace Generators
{
  internal static class CollectionFiller //todo make up a better name
  {
    public static ICollection<T> FillingCollection<T>(ICollection<T> collection, int many, IInstanceGenerator instanceGenerator)
    {
      for (int i = 0; i < many; ++i)
      {
        collection.Add(instanceGenerator.Instance<T>());
      }

      return collection;
    }
  }
}