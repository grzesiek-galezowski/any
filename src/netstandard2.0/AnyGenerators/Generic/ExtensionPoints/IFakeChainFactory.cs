using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IGenerationChain<T> GetInstance<T>();
    IGenerationChain<T> GetUnconstrainedInstance<T>();
    IResolution CreateFakeOrdinaryInterfaceGenerator();
  }
}
