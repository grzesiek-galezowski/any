using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class InclusiveEnumerableGenerator<T>(IEnumerable<T> included) : InlineGenerator<IEnumerable<T>>
{
  public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var list = new List<T>();
    list.Add(instanceGenerator.Instance<T>(request));
    list.AddRange(included);
    list.Add(instanceGenerator.Instance<T>(request));

    return list;
  }
}
