using System;
using System.Reflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails
{
  public class FieldWrapper : IFieldWrapper
  {
    private readonly FieldInfo _fieldInfo;

    public FieldWrapper(FieldInfo fieldInfo)
    {
      _fieldInfo = fieldInfo;
    }

    public void SetValue(object result, object instance)
    {
      _fieldInfo.SetValue(result, instance);
    }

    public Type FieldType => _fieldInfo.FieldType;

    public bool IsNullOrDefault(object result)
    {
      return Equals(_fieldInfo.GetValue(result), DefaultValue.Of(FieldType));
    }
  }

  public static class DefaultValue
  {
    public static object Of(Type t)
    {
      if (t.IsValueType)
      {
        return Activator.CreateInstance(t);
      }

      return null;
    }
  }
}