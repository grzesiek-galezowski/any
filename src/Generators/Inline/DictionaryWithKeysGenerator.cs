using System.Collections.Generic;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class DictionaryWithKeysGenerator<TKey, TValue> 
    : InlineGenerator<Dictionary<TKey, TValue>>
  {
    private readonly IEnumerable<TKey> _keys;

    public DictionaryWithKeysGenerator(IEnumerable<TKey> keys)
    {
      _keys = keys;
    }

    public Dictionary<TKey, TValue> GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var dict = new Dictionary<TKey, TValue>();

      foreach (var key in _keys)
      {
        dict.Add(key, instanceGenerator.InstanceOf<TValue>());
      }

      return dict;
    }

  }
}