using TddXt.AnyGenerators.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeChainFactory : IFakeChainFactory
  {
    private readonly IGenerationChain _generationChain;
    private readonly IGenerationChain _unconstrainedChain;
    private readonly FakeOrdinaryInterface _fakeOrdinaryInterfaceGenerator;

    public FakeChainFactory(
      IGenerationChain generationChain, 
      IGenerationChain unconstrainedInstance, 
      FakeOrdinaryInterface fakeOrdinaryInterfaceGenerator)
    {
      _generationChain = generationChain;
      _unconstrainedChain = unconstrainedInstance;
      _fakeOrdinaryInterfaceGenerator = fakeOrdinaryInterfaceGenerator;
    }

    public IGenerationChain GetInstance()
    {
      return _generationChain;
    }

    public IGenerationChain GetUnconstrainedInstance()
    {
      return _unconstrainedChain;
    }

    public IResolution CreateFakeOrdinaryInterfaceGenerator()
    {
      return _fakeOrdinaryInterfaceGenerator;
    }
  }
}
