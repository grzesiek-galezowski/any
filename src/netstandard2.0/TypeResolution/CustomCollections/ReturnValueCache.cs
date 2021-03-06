using System;
using System.Collections.Generic;

namespace TddXt.TypeResolution.CustomCollections
{
  [Serializable]
  public class PerMethodCache<T>
  {
    private readonly Dictionary<PerMethodCacheKey, T> _cache = new();

    public bool AlreadyContainsValueFor(PerMethodCacheKey cacheKey)
    {
      return _cache.ContainsKey(cacheKey);
    }

    public void Add(PerMethodCacheKey cacheKey, T cachedObject)
    {
      _cache.Add(cacheKey, cachedObject);
    }

    public T ValueFor(PerMethodCacheKey cacheKey)
    {
      return _cache[cacheKey];
    }

    public void Overwrite(PerMethodCacheKey key, T cachedObject)
    {
      _cache[key] = cachedObject;
    }
  }
}