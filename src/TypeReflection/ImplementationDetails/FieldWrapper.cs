using System;
using System.Reflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails;

public class FieldWrapper(FieldInfo fieldInfo) : IFieldWrapper
{
  public void SetValue(object result, object instance)
  {
    fieldInfo.SetValue(result, instance);
  }

  public Type FieldType => fieldInfo.FieldType;

  public bool IsNullOrDefault(object result)
  {
    return Equals(fieldInfo.GetValue(result), DefaultValue.Of(FieldType));
  }
}

public static class DefaultValue
{
  public static object? Of(Type t)
  {
    if (t.IsValueType)
    {
      return Activator.CreateInstance(t);
    }

    return null;
  }
}
