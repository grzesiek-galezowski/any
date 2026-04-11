using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class DictionaryWithKeysGenerator<TKey, TValue>(IEnumerable<TKey> keys)
  : InlineGenerator<Dictionary<TKey, TValue>> where TKey : notnull
{
  public Dictionary<TKey, TValue> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return keys.ToDictionary(key => key, key => instanceGenerator.Instance<TValue>(request));
  }
}
