using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Collections;

public class KeyValuePairGenerator<TKey, TValue> : InlineGenerator<KeyValuePair<TKey, TValue>>
{
  public KeyValuePair<TKey, TValue> GenerateInstance(InstanceGenerator gen, GenerationRequest request)
  {
    return new KeyValuePair<TKey, TValue>(gen.Instance<TKey>(request), gen.Instance<TValue>(request));
  }
}