using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T> //bug rename
  {
    T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request);
  }
}