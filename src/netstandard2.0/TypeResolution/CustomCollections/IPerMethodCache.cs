using System;

namespace TddXt.TypeResolution.CustomCollections;

public interface IPerMethodCache<T>
{
  T ValueFor(PerMethodCacheKey cacheKey);
  void Overwrite(PerMethodCacheKey key, T cachedObject);
  void AddIfNoValueFor(PerMethodCacheKey cacheKey, Func<T> source);
}
