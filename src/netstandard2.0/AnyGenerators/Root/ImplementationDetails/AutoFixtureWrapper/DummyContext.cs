using AutoFixture.Kernel;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.AutoFixtureWrapper;

public class DummyContext : ISpecimenContext
{
  public object? Resolve(object request)
  {
    return null;
  }
}
