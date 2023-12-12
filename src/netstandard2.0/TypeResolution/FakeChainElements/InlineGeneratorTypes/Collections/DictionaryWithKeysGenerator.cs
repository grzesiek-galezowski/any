using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class DictionaryWithKeysGenerator<TKey, TValue>(IEnumerable<TKey> keys)
  : InlineGenerator<Dictionary<TKey, TValue>>
{
  public Dictionary<TKey, TValue> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var dict = new Dictionary<TKey, TValue>();

    foreach (var key in keys)
    {
      dict.Add(key, instanceGenerator.Instance<TValue>(request));
    }

    return dict;
  }
}
