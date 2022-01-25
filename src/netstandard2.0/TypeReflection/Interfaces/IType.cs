using System;
using System.Collections.Generic;
using Core.Maybe;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IType
  {
    bool HasPublicParameterlessConstructor();
    bool IsImplementationOfOpenGeneric(Type openGenericType);
    bool IsConcrete();
    Maybe<IConstructorWrapper> PickConstructorWithLeastNonPointersParameters();
    IEnumerable<IConstructorWrapper> GetAllPublicConstructors();
    IEnumerable<IFieldWrapper> GetAllPublicInstanceFields();
    IEnumerable<IPropertyWrapper> GetPublicInstanceWritableProperties();
    bool IsException();
    bool HasPublicConstructorCountOfAtMost(int i);
    bool IsOpenGeneric(Type type);
    void AssertMatchesTypeOf(object instance);
  }
}
