using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IGenerationChain GetInstance();
    IGenerationChain GetUnconstrainedInstance();
    IResolution CreateFakeOrdinaryInterfaceGenerator();
  }
}
