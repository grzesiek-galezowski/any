using System;
using System.Collections;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeEnumerator<T> : IResolution<T>
  {
    public bool AppliesTo(Type type)
    {
      return SmartType.For(type).Is(typeof(IEnumerator));
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return (T)(instanceGenerator.Instance<object[]>(request).GetEnumerator());
    }
  }
}
