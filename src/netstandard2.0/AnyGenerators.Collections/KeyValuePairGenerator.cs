using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class KeyValuePairGenerator<TKey, TValue> : InlineGenerator<KeyValuePair<TKey, TValue>>
  {
    public KeyValuePair<TKey, TValue> GenerateInstance(InstanceGenerator gen, GenerationTrace trace)
    {
      return new KeyValuePair<TKey, TValue>(gen.Instance<TKey>(trace), gen.Instance<TValue>(trace));
    }
  }
}