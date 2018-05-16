using TddEbook.TddToolkit.Generators;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public interface IFakeChainFactory
  {
    IFakeChain<T> GetInstance<T>();
    IFakeChain<T> GetUnconstrainedInstance<T>();
    ISpecialCasesOfResolutions<T> CreateSpecialCasesOfResolutions<T>();
    IResolution<T> CreateFakeOrdinaryInterfaceGenerator<T>();
  }
}