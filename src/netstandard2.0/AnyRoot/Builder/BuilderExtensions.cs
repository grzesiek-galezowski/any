using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Core.NullableReferenceTypesExtensions;

namespace TddXt.AnyRoot.Builder;

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
      if (memberSelectorExpression.Member is PropertyInfo propertyInfo)
      {
        SetValue(TargetParentObject(target, memberSelectorExpression), value, propertyInfo);
      }
      else if (memberSelectorExpression.Member is FieldInfo fieldInfo)
      {
        SetValue(TargetParentObject(target, memberSelectorExpression), value, fieldInfo);
      }
    }

    return target;
  }

  private static object TargetParentObject(object target, MemberExpression memberSelectorExpression)
  {
    if (memberSelectorExpression.Expression is not MemberExpression parentExpression)
    {
      return target;
    }
    else
    {
      if (parentExpression.Member is PropertyInfo propertyInfo)
      {
        return propertyInfo.GetValue(target) ?? throw new Exception("Trying to set property on null");
      }
      else if (parentExpression.Member is FieldInfo fieldInfo)
      {
        return fieldInfo.GetValue(target) ?? throw new Exception("Trying to set field value on null");
      }
      else
      {
        throw new Exception("unrecognized member type " + parentExpression.Member.MemberType);
      }
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
        var field = property.DeclaringType.OrThrow().GetField($"<{property.Name}>k__BackingField",
          BindingFlags.Instance | BindingFlags.NonPublic).OrThrow();
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

  private static void SetValue<T, TValue>(T target, TValue value, FieldInfo? fieldInfo)
  {
    if (fieldInfo != null)
    {
      fieldInfo.SetValue(target, value);
    }
    else
    {
      throw new Exception("Could not find the field");
    }
  }

  private static bool IsAutoProperty(this PropertyInfo prop)
  {
    return prop.DeclaringType
      .OrThrow()
      .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
      .OrThrow()
      .Any(f => f.Name.Contains("<" + prop.Name + ">"));
  }
}
