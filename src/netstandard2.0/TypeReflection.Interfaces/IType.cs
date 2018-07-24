using System;
using System.Collections.Generic;
using CommonTypes;

namespace TypeReflection.Interfaces
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
    IEnumerable<IMethod> GetAllPublicInstanceMethodsWithReturnValue();
    bool IsException();
    bool HasPublicConstructorCountOfAtMost(int i);
    bool IsOpenGeneric(Type type);
  }
}