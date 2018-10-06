using System;
using System.Linq;
using System.Reflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection
{
  public class SmartMethod : IMethod
  {
    private readonly MethodInfo _methodInfo;

    public SmartMethod(MethodInfo methodInfo)
    {
      _methodInfo = methodInfo;
    }

    public object InvokeWithAnyArgsOn(object instance, Func<Type, object> valueFactory)
    {
      var parameters = GenerateAnyValuesFor(valueFactory);
      return _methodInfo.Invoke(instance, parameters);
    }

    public object GenerateAnyReturnValue(Func<Type, object> valueFactory)
    {
      return valueFactory.Invoke(_methodInfo.ReturnType);
    }

    private object[] GenerateAnyValuesFor(Func<Type, object> valueFactory)
    {
      return _methodInfo.GetParameters().Select(p => p.ParameterType).Select(valueFactory).ToArray();
    }
  }
}