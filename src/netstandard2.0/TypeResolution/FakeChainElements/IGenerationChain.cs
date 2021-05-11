using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IGenerationChain<out T>
  {
    T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request);
  }
}
