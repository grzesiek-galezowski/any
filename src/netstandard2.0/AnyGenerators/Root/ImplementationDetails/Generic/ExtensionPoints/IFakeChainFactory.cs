using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Generic.ExtensionPoints;

public interface IFakeChainFactory
{
  IGenerationChain GetInstance();
  IGenerationChain GetUnconstrainedInstance();
  IResolution CreateFakeOrdinaryInterfaceGenerator();
}