using System;
using NUnit.Framework.Legacy;
using TddXt.TypeReflection;

namespace AnySpecification;

public class TypeOfTypeSpecification
{
  [Test, Parallelizable]
  public void ShouldCorrectlyDetermineIfObjectIsOfTypeType() //this is not a typo!
  {
    Assert.Multiple(() =>
    {
      ClassicAssert.False(TypeOfType.Is<object>());
      ClassicAssert.False(TypeOfType.Is<int>());
      ClassicAssert.True(TypeOfType.Is<Type>());
    });
  }
}
