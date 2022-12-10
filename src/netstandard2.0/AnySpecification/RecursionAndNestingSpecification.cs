using AnySpecification.Fixtures;

namespace AnySpecification;

public class RecursionAndNestingSpecification
{
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    Assert.NotNull(instance.Same.Same.Same, "Last recursive element should be generated as uninitialized dummy");
    Assert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
    Assert.NotNull(instance.Same.Same.Same.Whatever, "Whatever doesn't increase recursion count");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType2()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItself>();

    Assert.NotNull(instance.Same.Same.Same, "Fourth recursive element should be generated as uninitialized dummy");
    Assert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType3()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItselfInConstructor>();

    Assert.NotNull(instance.Same.Same.Same, "Fourth recursive element should be generated as uninitialized dummy");
    Assert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
  }

  [Test, Parallelizable]
  public void ShouldApplyPerTypeRecursionLimit()
  {
    //GIVEN
    var instance = Any.Instance<ObjectWithIndirectRecursion>();

    Assert.NotNull(instance.Other2.Other.Other2.Other.Other2.Other.Other2, "Dummy algorithm generates one last dummy");
    Assert.Null(instance.Other2.Other.Other2.Other.Other2.Other.Other2.Other);
  }

  [Test, Parallelizable]
  public void ShouldRespectNestingLimit()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    //THEN
    Assert.AreEqual(1, instance.Others[0].Other.Others[0].Other.Others.Length, "Dummy algorithm generates an empty collection");
  }

}
