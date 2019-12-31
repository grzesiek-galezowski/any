#if NETFRAMEWORK
using Ploeh.AutoFixture.Kernel;
#else
using AutoFixture.Kernel;
#endif

namespace TddXt.AutoFixtureWrapper
{
  public class DummyContext : ISpecimenContext
  {
    public object? Resolve(object request)
    {
      return null;
    }
  }
}

