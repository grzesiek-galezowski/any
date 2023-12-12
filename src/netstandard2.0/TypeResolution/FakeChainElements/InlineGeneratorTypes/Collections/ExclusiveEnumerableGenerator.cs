using System.Collections.Generic;
using TddXt.AnyExtensibility;
using static System.Linq.Enumerable;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class ExclusiveEnumerableGenerator<T>(T[] excluded, int many) : InlineGenerator<IEnumerable<T>>
{
  public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return Range(0, many).Select(i => instanceGenerator.OtherThan(excluded)).ToList();
  }
}
