using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(InstanceGenerator instanceGenerator)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}