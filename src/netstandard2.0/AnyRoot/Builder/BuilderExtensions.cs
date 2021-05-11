using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TddXt.AnyRoot.Builder
{
  public static class BuilderExtensions
  {
    public static T WithProperty<T, TValue>(this T target, Expression<Func<T, TValue>> memberLambda, TValue value)
    where T : notnull
    {
      if (target == null)
      {
        throw new Exception("Cannot set a property on null");
      }
      if (memberLambda.Body is MemberExpression memberSelectorExpression)
      {
        var property = memberSelectorExpression.Member as PropertyInfo;
        SetValue(Target(target, memberSelectorExpression), value, property);
      }

      return target;
    }

    private static object Target(object target, MemberExpression memberSelectorExpression)
    {
      var parentExpression = memberSelectorExpression.Expression as MemberExpression;
      if (parentExpression == null)
      {
        return target;
      }
      else
      {
        var propertyInfo = (PropertyInfo)parentExpression.Member;
        return propertyInfo.GetValue(target) ?? throw new Exception("Trying to set property on null");
      }
    }

    private static void SetValue<T, TValue>(T target, TValue value, PropertyInfo? property)
    {
      if (property != null)
      {
        if (property.CanWrite)
        {
          property.SetValue(target, value);
        }
        else if (IsAutoProperty(property))
        {
          var field = property.DeclaringType.GetField($"<{property.Name}>k__BackingField",
              BindingFlags.Instance | BindingFlags.NonPublic);
          field.SetValue(target, value);
        }
        else
        {
          throw new Exception($"The property {property.Name} is neither writable nor auto-property");
        }
      }
      else
      {
        throw new Exception("Could not find the property");
      }
    }

    private static bool IsAutoProperty(this PropertyInfo prop)
    {
      return prop.DeclaringType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
          .Any(f => f.Name.Contains("<" + prop.Name + ">"));
    }
  }
}
