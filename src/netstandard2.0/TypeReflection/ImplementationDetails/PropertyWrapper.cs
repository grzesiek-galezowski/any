using System;
using System.Reflection;
using Core.Maybe;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails;

public class PropertyWrapper(PropertyInfo propertyInfo) : IPropertyWrapper
{
  public bool HasAbstractGetter()
  {
    var getter = propertyInfo.GetGetMethod();
    return getter is not null && getter.IsAbstract;
  }

  public Type PropertyType { get { return propertyInfo.PropertyType; } }

  public void SetValue(object result, object value)
  {
    propertyInfo.SetValue(result, value, null);
  }

  public bool HasPublicSetter()
  {
    return propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic;
  }

  public Maybe<object> GetValue(object generatedObject)
  {
    return propertyInfo.GetValue(generatedObject, null).ToMaybe();
  }
}
