using System;
using System.Collections.Generic;
using System.Reflection;

namespace TddXt.AnyExtensibility
{
  public interface GenerationTrace
  {
    void AddNestingAndCheckWith(int nesting, Type type);
    void RemoveNestingAndCheckWith(int nesting, Type type);
    void GeneratingSeedeedValue<T>(Type type, T seed);
    void SelectedResolution(Type type, object resolution);
    void NestingLimitReachedTryingDummy();
    void ThirdPartyGeneratorFailedTryingFallback(Exception exception);
    void ChosenParameterlessConstructor();
    void ChosenConstructor(string constructorName, IEnumerable<TypeInfo> parameterTypes);
    string ToString();
  }
}