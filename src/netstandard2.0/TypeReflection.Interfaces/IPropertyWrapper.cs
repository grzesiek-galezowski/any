using System;

namespace TddXt.TypeReflection.Interfaces
{
  public interface IPropertyWrapper
  {
    bool HasAbstractGetter();
    Type PropertyType { get; }
    void SetValue(object result, object value);
  }
}
