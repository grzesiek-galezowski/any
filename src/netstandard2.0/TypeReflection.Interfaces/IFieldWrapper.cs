using System;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    void SetValue(object result, object instance);
    Type FieldType { get; }
  }
}
