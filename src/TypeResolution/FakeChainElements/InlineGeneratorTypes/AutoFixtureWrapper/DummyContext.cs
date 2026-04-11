using AutoFixture.Kernel;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

public class DummyContext : ISpecimenContext
{
  public object? Resolve(object request)
  {
    return null;
  }
}
