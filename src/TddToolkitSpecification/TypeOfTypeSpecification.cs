using System;
using NUnit.Framework;
using TddEbook.TddToolkit;
using TddEbook.TypeReflection;
using Type = System.Type;

namespace TddEbook.TddToolkitSpecification
{
  public class TypeOfTypeSpecification
  {
    [Test]
    public void ShouldCorrectlyDetermineIfObjectIsOfTypeType() //this is not a typo!
    {
      Assert.Multiple(() =>
      {
        Assert.False(TypeOfType.Is<object>());
        Assert.False(TypeOfType.Is<int>());
        Assert.True(TypeOfType.Is<Type>());
      });
    }
  }
}
