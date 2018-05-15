using System.Collections;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
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