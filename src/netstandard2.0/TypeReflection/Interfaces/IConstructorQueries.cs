using System.Collections.Generic;
using TddXt.CommonTypes;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IConstructorQueries
  {
    Maybe<IConstructorWrapper> GetNonPublicParameterlessConstructorInfo();
    Maybe<IConstructorWrapper> GetPublicParameterlessConstructor();
    List<IConstructorWrapper> TryToObtainInternalConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithoutRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPublicConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainInternalConstructorsWithRecursiveArguments();
    IEnumerable<IConstructorWrapper> TryToObtainPrimitiveTypeConstructor();
    IEnumerable<IConstructorWrapper> TryToObtainPublicStaticFactoryMethodWithoutRecursion();
    IEnumerable<IConstructorWrapper> TryToObtainPrivateAndProtectedConstructorsWithoutRecursiveArguments();
  }
}