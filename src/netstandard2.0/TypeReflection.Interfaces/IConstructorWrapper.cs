using System;
using System.Collections.Generic;
using System.Reflection;
using TddXt.CommonTypes;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IConstructorWrapper
  {
    bool HasNonPointerArgumentsOnly();
    bool HasLessParametersThan(int numberOfParams);
    int GetParametersCount();
    bool HasAbstractOrInterfaceArguments();
    List<object> GenerateAnyParameterValues(Func<Type, GenerationTrace, object> instanceGenerator,
      GenerationTrace trace);
    bool IsParameterless();
    object InvokeWithParametersCreatedBy(Func<Type, GenerationTrace, object> instanceGenerator, GenerationTrace trace);
    bool IsInternal();
    bool IsNotRecursive();
    bool IsRecursive();

    object Invoke(IEnumerable<object> parameters);
    IEnumerable<ParameterInfo> Parameters { get; }

    void DumpInto(GenerationTrace trace);
  }
}
