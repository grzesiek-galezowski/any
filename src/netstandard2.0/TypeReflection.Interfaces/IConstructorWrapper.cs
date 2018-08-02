using System;
using System.Collections.Generic;
using System.Reflection;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IConstructorWrapper
  {
    bool HasNonPointerArgumentsOnly();
    bool HasLessParametersThan(int numberOfParams);
    int GetParametersCount();
    bool HasAbstractOrInterfaceArguments();
    List<object> GenerateAnyParameterValues(Func<Type, object> instanceGenerator);
    bool IsParameterless();
    object InvokeWithParametersCreatedBy(Func<Type, object> instanceGenerator);
    bool IsInternal();
    bool IsNotRecursive();
    bool IsRecursive();

    object Invoke(IEnumerable<object> parameters);
    IEnumerable<ParameterInfo> Parameters { get; }

  }
}
