using System;
using TddXt.TypeReflection;

namespace AnySpecification;

public class TypeOfTypeSpecification
{
  [Test, Parallelizable]
  public void ShouldCorrectlyDetermineIfObjectIsOfTypeType() //this is not a typo!
  {
    using (Assert.EnterMultipleScope())
    {
      Assert.That(TypeOfType.Is<object>(), Is.False);
      Assert.That(TypeOfType.Is<int>(), Is.False);
      Assert.That(TypeOfType.Is<Type>(), Is.True);
    }
  }
}
