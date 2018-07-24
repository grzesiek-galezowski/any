using System;
using System.Linq;
using System.Reflection;
using TypeReflection.Interfaces;

namespace TypeReflection
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

    private object[] GenerateAnyValuesFor(Func<Type, object> valueFactory)
    {
      return _methodInfo.GetParameters().Select(p => p.ParameterType).Select(valueFactory).ToArray();
    }

    public object GenerateAnyReturnValue(Func<Type, object> valueFactory)
    {
      return valueFactory.Invoke(_methodInfo.ReturnType);
    }

  }
}