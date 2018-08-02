using System;
using System.Text;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IFieldWrapper
  {
    void SetValue(object result, object instance);
    Type FieldType { get; }
    bool HasName(string name);
    bool HasValue(object name);
    void AddNameTo(StringBuilder builder);
  }
}
