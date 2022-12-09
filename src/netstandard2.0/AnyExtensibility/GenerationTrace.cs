using System;
using System.Collections.Generic;
using System.Reflection;

namespace TddXt.AnyExtensibility;

public interface GenerationTrace
{
  void AddNestingAndCheckWith(int nesting, Type type);
  void RemoveNestingAndCheckWith(int nesting, Type type);
  void GeneratingSeededValue<T>(Type type, T seed);
  void SelectedResolution(Type type, object resolution);
  void RecursionLimitReachedTryingDummy();
  void ThirdPartyGeneratorFailedTryingFallback(Exception exception);
  void ChosenParameterlessConstructor();
  void ChosenConstructor(string constructorName, IEnumerable<TypeInfo> parameterTypes);
  void BeginCreatingInstanceGraphWith(Type type);
  string ToString();
  void BeginCreatingInstanceGraphWithInlineGenerator(Type type, object gen);
  void BeginCreatingDummyInstanceOf(Type type);
}