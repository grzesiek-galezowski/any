using AnySpecification.Fixtures;
using Core.NullableReferenceTypesExtensions;

namespace AnySpecification;

public class RecursionAndNestingSpecification
{
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    Assert.That(instance.Same.OrThrow().Same.OrThrow().Same, Is.Not.Null, "Last recursive element should be generated as uninitialized dummy");
    Assert.That(instance.Same.OrThrow().Same.OrThrow().Same.OrThrow().Same, Is.Null, "Semi-last is dummy");
    Assert.That(instance.Same.OrThrow().Same.OrThrow().Same.OrThrow().Whatever, Is.Not.Null, "Whatever doesn't increase recursion count");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType2()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItself>();

    Assert.That(instance.Same.OrThrow().Same.OrThrow().Same, Is.Not.Null, "Fourth recursive element should be generated as uninitialized dummy");
    Assert.That(instance.Same.OrThrow().Same.OrThrow().Same.OrThrow().Same, Is.Null, "Semi-last is dummy");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType3()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItselfInConstructor>();

    Assert.That(instance.Same.Same.Same, Is.Not.Null, "Fourth recursive element should be generated as uninitialized dummy");
    Assert.That(instance.Same.Same.Same.Same, Is.Null, "Semi-last is dummy");
  }

  [Test, Parallelizable]
  public void ShouldApplyPerTypeRecursionLimit()
  {
    //GIVEN
    var instance = Any.Instance<ObjectWithIndirectRecursion>();

    Assert.That(
      instance.Other2.OrThrow().Other.OrThrow().Other2.OrThrow().Other.OrThrow().Other2.OrThrow().Other.OrThrow()
        .Other2, Is.Not.Null, "Dummy algorithm generates one last dummy");
    Assert.That(instance.Other2.OrThrow().Other.OrThrow().Other2.OrThrow().Other.OrThrow().Other2.OrThrow().Other
      .OrThrow().Other2.OrThrow().Other, Is.Null);
  }

  [Test, Parallelizable, Ignore("diminishing disabled for now for backwards compatibility reasons")]
  public void ShouldRespectNestingLimit()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    //THEN
    Assert.That(instance.Others.OrThrow()[0].Other.OrThrow().Others.OrThrow()[0].Other.OrThrow().Others.OrThrow().Length,
      Is.EqualTo(1),
      "Dummy algorithm generates an empty collection");
  }

}
