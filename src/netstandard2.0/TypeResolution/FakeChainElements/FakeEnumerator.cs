using System.Collections;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeEnumerator<T> : IResolution<T>
  {

    public bool Applies()
    {
      return TypeOf<T>.Is<IEnumerator>();
    }

    public T Apply(InstanceGenerator instanceGenerator)
    {
      return (T)(instanceGenerator.Instance<object[]>().GetEnumerator());
    }
  }
}