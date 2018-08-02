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

    public Type FieldType { get { return _fieldInfo.FieldType; } }
  }
}