using System;
using System.Collections.Generic;

namespace TddXt.TypeResolution.CustomCollections;

[Serializable]
public class PerMethodCache<T> : IPerMethodCache<T>
{
  private readonly Dictionary<PerMethodCacheKey, T> _cache = [];

  public T ValueFor(PerMethodCacheKey cacheKey)
  {
    return _cache[cacheKey];
  }

  public void Overwrite(PerMethodCacheKey key, T cachedObject)
  {
    _cache[key] = cachedObject;
  }

  public void AddIfNoValueFor(PerMethodCacheKey cacheKey, Func<T> source)
  {
    if (!AlreadyContainsValueFor(cacheKey))
    {
      Add(cacheKey, source());
    }
  }

  private bool AlreadyContainsValueFor(PerMethodCacheKey cacheKey)
  {
    return _cache.ContainsKey(cacheKey);
  }

  private void Add(PerMethodCacheKey cacheKey, T cachedObject)
  {
    _cache.Add(cacheKey, cachedObject);
  }
}
