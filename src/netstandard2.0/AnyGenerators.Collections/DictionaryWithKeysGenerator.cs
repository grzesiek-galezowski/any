using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class DictionaryWithKeysGenerator<TKey, TValue> 
    : InlineGenerator<Dictionary<TKey, TValue>>
  {
    private readonly IEnumerable<TKey> _keys;

    public DictionaryWithKeysGenerator(IEnumerable<TKey> keys)
    {
      _keys = keys;
    }

    public Dictionary<TKey, TValue> GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var dict = new Dictionary<TKey, TValue>();

      foreach (var key in _keys)
      {
        dict.Add(key, instanceGenerator.Instance<TValue>(trace));
      }

      return dict;
    }
  }
}