using System;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    Type FieldType { get; }
    void SetValue(object result, object instance);
    bool IsNullOrDefault(object result);
  }
}
