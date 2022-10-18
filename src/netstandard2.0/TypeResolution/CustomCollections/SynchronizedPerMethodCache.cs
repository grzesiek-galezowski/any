using System;

namespace TddXt.TypeResolution.CustomCollections;

public class SynchronizedPerMethodCache<T> : IPerMethodCache<T>
{
  private readonly object _syncRoot = new object();
  private readonly IPerMethodCache<T> _inner;

  public SynchronizedPerMethodCache(IPerMethodCache<T> inner)
  {
    _inner = inner;
  }

  public T ValueFor(PerMethodCacheKey cacheKey)
  {
    lock(_syncRoot) return _inner.ValueFor(cacheKey);
  }

  public void Overwrite(PerMethodCacheKey key, T cachedObject)
  {
    lock(_syncRoot) _inner.Overwrite(key, cachedObject);
  }

  public void AddIfNoValueFor(PerMethodCacheKey cacheKey, Func<T> source)
  {
    lock(_syncRoot) _inner.AddIfNoValueFor(cacheKey, source);
  }
}
