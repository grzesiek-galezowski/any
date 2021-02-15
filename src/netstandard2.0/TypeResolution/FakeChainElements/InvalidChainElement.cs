using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class InvalidChainElement<T> : IChainElement<T>
  {
    public T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      throw new ChainFailedException(typeof(T));
    }
  }
}