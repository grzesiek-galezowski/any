using System;
using System.Reflection;
using System.Text;
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

    public bool HasName(string name)
    {
      return _fieldInfo.Name == name;
    }

    public bool HasValue(object name)
    {
      return _fieldInfo.GetValue(null).Equals(name);
    }

    public void AddNameTo(StringBuilder builder)
    {
      builder.Append(_fieldInfo.Name + " <" + _fieldInfo.GetValue(null) + ">");
    }
  }
}