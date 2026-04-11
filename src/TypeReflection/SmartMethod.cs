using System;
using System.Linq;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection;

public class SmartMethod(MethodInfo methodInfo) : IMethod
{
  public object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory)
  {
    var parameters = GenerateAnyValuesFor(valueFactory);
    return methodInfo.Invoke(instance, parameters).OrThrow();
  }

  public object GenerateAnyReturnValue(Func<Type, object> valueFactory)
  {
    return valueFactory.Invoke(methodInfo.ReturnType);
  }

  private object[] GenerateAnyValuesFor(Func<Type, object> valueFactory)
  {
    return methodInfo.GetParameters().Select(p => p.ParameterType).Select(valueFactory).ToArray();
  }
}
