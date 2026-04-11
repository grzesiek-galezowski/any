using System;
using Core.Maybe;

namespace TddXt.TypeReflection.Interfaces;

public interface IPropertyWrapper
{
  Type PropertyType { get; }
  bool HasAbstractGetter();
  void SetValue(object result, object value);
  bool HasPublicSetter();
  Maybe<object> GetValue(object generatedObject);
}
