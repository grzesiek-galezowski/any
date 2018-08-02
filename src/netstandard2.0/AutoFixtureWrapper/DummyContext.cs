using AutoFixture.Kernel;

namespace TddXt.AutoFixtureWrapper
{
  public class DummyContext : ISpecimenContext
  {
    public object Resolve(object request)
    {
      return null;
    }
  }
}

