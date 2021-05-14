using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IGenerationChain GetInstance<T>();
    IGenerationChain GetUnconstrainedInstance<T>();
    ISpecialCasesOfResolutions CreateSpecialCasesOfResolutions<T>();
    IResolution CreateFakeOrdinaryInterfaceGenerator<T>();
  }
}
