using System;

namespace TddXt.TypeResolution.CustomCollections;

public class SynchronizedPerMethodCache<T>(IPerMethodCache<T> inner) : IPerMethodCache<T>
{
  private readonly object _syncRoot = new();

  public T ValueFor(PerMethodCacheKey cacheKey)
  {
    lock(_syncRoot) return inner.ValueFor(cacheKey);
  }

  public void Overwrite(PerMethodCacheKey key, T cachedObject)
  {
    lock(_syncRoot) inner.Overwrite(key, cachedObject);
  }

  public void AddIfNoValueFor(PerMethodCacheKey cacheKey, Func<T> source)
  {
    lock(_syncRoot) inner.AddIfNoValueFor(cacheKey, source);
  }
}
