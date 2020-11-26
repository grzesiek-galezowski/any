using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IFakeChain<T> GetInstance<T>();
    IFakeChain<T> GetUnconstrainedInstance<T>();
    ISpecialCasesOfResolutions<T> CreateSpecialCasesOfResolutions<T>();
    IResolution<T> CreateFakeOrdinaryInterfaceGenerator<T>();
  }
}