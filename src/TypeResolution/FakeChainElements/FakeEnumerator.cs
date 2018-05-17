using System.Collections;
using TddXt.AnyExtensibility;
using TypeReflection;

namespace TypeResolution.FakeChainElements
{
  public class FakeEnumerator<T> : IResolution<T>
  {
    public FakeEnumerator()
    {
    }

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