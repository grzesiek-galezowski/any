using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IGenerationChain<T> GetInstance<T>();
    IGenerationChain<T> GetUnconstrainedInstance<T>();
    ISpecialCasesOfResolutions<T> CreateSpecialCasesOfResolutions<T>();
    IResolution<T> CreateFakeOrdinaryInterfaceGenerator<T>();
  }
}