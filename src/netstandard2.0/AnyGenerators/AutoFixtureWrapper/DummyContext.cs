using AutoFixture.Kernel;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  public class DummyContext : ISpecimenContext
  {
    public object? Resolve(object request)
    {
      return null;
    }
  }
}

