using System;
using System.Collections;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeEnumerator : IResolution
  {
    public bool AppliesTo(Type type)
    {
      return SmartType.For(type).Is(typeof(IEnumerator));
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return instanceGenerator.Instance<object[]>(request).GetEnumerator();
    }
  }
}
