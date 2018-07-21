using Ploeh.AutoFixture.Kernel;

namespace AutoFixtureWrapper
{
  public class DummyContext : ISpecimenContext
  {
    public object Resolve(object request)
    {
      return null;
    }
  }
}

