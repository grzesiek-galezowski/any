using System;
using AnySpecification.Fixtures;
using TddXt.AnyExtensibility;
using TddXt.AnyRoot.Exploding;
using TddXt.TypeResolution.FakeChainElements.Interceptors;

namespace AnySpecification;

public class ExplodingSpecification
{
  [Test, Parallelizable]
  public void ShouldCreateAnExplodingProxyForInterfaces()
  {
    //GIVEN
    var exploding = Any.Exploding<ISimple>();

    //WHEN
    var exception = Assert.Throws<BoooooomException>(() => exploding.GetInt());

    //THEN
    Assert.That(exception!.Message, Does.Contain("GetInt"));
  }

  [Test, Parallelizable]
  public void ShouldRejectExplodingConcreteTypes()
  {
    //WHEN
    var exception = Assert.Throws<GenerationFailedException>(() => Any.Exploding<ThrowingInConstructor>());

    //THEN
    Assert.That(exception!.InnerException!.Message, Is.EqualTo("Exploding instances can be created out of interfaces only!"));
  }
}
