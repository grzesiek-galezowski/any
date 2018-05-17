using AutoFixture.Kernel;

namespace TddXt.AnyGenerators.Strings
{
  public class DummyContext : ISpecimenContext
  {
    public object Resolve(object request)
    {
      return null;
    }
  }
}

