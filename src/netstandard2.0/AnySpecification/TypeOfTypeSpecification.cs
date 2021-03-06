﻿using System;
using NUnit.Framework;
using TddXt.TypeReflection;

namespace AnySpecification
{
  public class TypeOfTypeSpecification
  {
    [Test, Parallelizable]
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
