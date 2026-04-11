using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class SimpleValueOtherThanGenerator<T>(T[] excluded) : InlineGenerator<T>
{
  private readonly object[] _excluded = excluded.Cast<object>().ToArray();

  public T GenerateInstance(InstanceGenerator gen, GenerationRequest request) => (T)gen.OtherThan(typeof(T), _excluded, request);
}
