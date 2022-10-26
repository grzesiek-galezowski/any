using System;
using System.Collections.Generic;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeReflection.Interfaces;

public interface IConstructorWrapper
{
  IEnumerable<ParameterInfo> Parameters { get; }
  bool HasNonPointerArgumentsOnly();
  bool HasLessParametersThan(int numberOfParams);
  int GetParametersCount();
  bool HasAbstractOrInterfaceArguments();

  List<object> GenerateAnyParameterValues(Func<Type, GenerationRequest, object> instanceGenerator,
    GenerationRequest request);

  bool IsParameterless();
  object InvokeWithParametersCreatedBy(Func<Type, GenerationRequest, object> instanceGenerator, GenerationRequest request);
  bool IsInternal();
  bool IsNotRecursive();
  bool IsRecursive();

  object Invoke(IEnumerable<object> parameters);

  void LogInScopeOf(GenerationRequest request);
}
