using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TddToolkitSpecification.Fixtures
{
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IObservableConcurrentDictionary<TKey, TValue>
    : IObservable<Tuple<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
  {
    TValue this[TKey key] { get; set; }
    void TryAdd(TKey key, TValue value);
    void TryRemove(TKey key);
    bool TryGetValue(TKey key, out TValue value);
  }
}