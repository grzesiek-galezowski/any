using System;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic.ExtensionPoints
{
  public interface IFakeChainFactory
  {
    IGenerationChain GetInstance<T>(Type type);
    IGenerationChain GetUnconstrainedInstance(Type type);
    IResolution CreateFakeOrdinaryInterfaceGenerator();
  }
}
