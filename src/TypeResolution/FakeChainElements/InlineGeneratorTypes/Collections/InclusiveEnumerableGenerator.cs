using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class InclusiveEnumerableGenerator<T>(IEnumerable<T> included) : InlineGenerator<IEnumerable<T>>
{
  public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    List<T> list = [
      instanceGenerator.Instance<T>(request), 
      ..included, 
      instanceGenerator.Instance<T>(request)];

    return list;
  }
}
