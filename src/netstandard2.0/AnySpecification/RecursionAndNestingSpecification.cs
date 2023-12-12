using AnySpecification.Fixtures;
using NUnit.Framework.Legacy;

namespace AnySpecification;

public class RecursionAndNestingSpecification
{
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    ClassicAssert.NotNull(instance.Same.Same.Same, "Last recursive element should be generated as uninitialized dummy");
    ClassicAssert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
    ClassicAssert.NotNull(instance.Same.Same.Same.Whatever, "Whatever doesn't increase recursion count");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType2()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItself>();

    ClassicAssert.NotNull(instance.Same.Same.Same, "Fourth recursive element should be generated as uninitialized dummy");
    ClassicAssert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
  }
  
  [Test, Parallelizable]
  public void ShouldRespectRecursionLimitOf5ForSpecificType3()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClassWithOnlyReferenceToItselfInConstructor>();

    ClassicAssert.NotNull(instance.Same.Same.Same, "Fourth recursive element should be generated as uninitialized dummy");
    ClassicAssert.Null(instance.Same.Same.Same.Same, "Semi-last is dummy");
  }

  [Test, Parallelizable]
  public void ShouldApplyPerTypeRecursionLimit()
  {
    //GIVEN
    var instance = Any.Instance<ObjectWithIndirectRecursion>();

    ClassicAssert.NotNull(instance.Other2.Other.Other2.Other.Other2.Other.Other2, "Dummy algorithm generates one last dummy");
    ClassicAssert.Null(instance.Other2.Other.Other2.Other.Other2.Other.Other2.Other);
  }

  [Test, Parallelizable, Ignore("diminishing disabled for now for backwards compatibility reasons")]
  public void ShouldRespectNestingLimit()
  {
    //GIVEN
    var instance = Any.Instance<RecursiveClass>();

    //THEN
    ClassicAssert.AreEqual(1, instance.Others[0].Other.Others[0].Other.Others.Length, "Dummy algorithm generates an empty collection");
  }

}
