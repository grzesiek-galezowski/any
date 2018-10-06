using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}