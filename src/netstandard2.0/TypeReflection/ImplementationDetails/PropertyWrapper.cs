using System;
using System.Reflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection.ImplementationDetails
{
  public class PropertyWrapper : IPropertyWrapper
  {
    private readonly PropertyInfo _propertyInfo;

    public PropertyWrapper(PropertyInfo propertyInfo)
    {
      _propertyInfo = propertyInfo;
    }

    public bool HasAbstractGetter()
    {
      return _propertyInfo.GetGetMethod().IsAbstract;
    }

    public Type PropertyType { get { return _propertyInfo.PropertyType; } }

    public void SetValue(object result, object value)
    {
      _propertyInfo.SetValue(result, value, null);
    }
  }
}