using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AnySpecification.Fixtures;

public class MyOwnPcCollection<T> : IProducerConsumerCollection<T>
{
  private readonly IProducerConsumerCollection<T> _inner = new ConcurrentBag<T>();
  public IEnumerator<T> GetEnumerator()
  {
    return _inner.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return ((IEnumerable)_inner).GetEnumerator();
  }

  public void CopyTo(Array array, int index)
  {
    _inner.CopyTo(array, index);
  }

  public int Count => _inner.Count;

  public bool IsSynchronized => _inner.IsSynchronized;

  public object SyncRoot => _inner.SyncRoot;

  public void CopyTo(T[] array, int index)
  {
    _inner.CopyTo(array, index);
  }

  public T[] ToArray()
  {
    return _inner.ToArray();
  }

  public bool TryAdd(T item)
  {
    return _inner.TryAdd(item);
  }

  public bool TryTake([MaybeNullWhen(false)] out T item)
  {
    return _inner.TryTake(out item);
  }
}
