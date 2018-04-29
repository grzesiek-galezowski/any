using System.Collections.Generic;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

public class KeyValuePairGenerator<TKey, TValue> : InlineGenerator<KeyValuePair<TKey, TValue>>
{
  public KeyValuePair<TKey, TValue> GenerateInstance(InstanceGenerator gen)
  {
    return new KeyValuePair<TKey, TValue>(gen.Instance<TKey>(), gen.Instance<TValue>());
  }
}