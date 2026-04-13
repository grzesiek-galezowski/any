using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection;

namespace TddXt.AnyRoot.Enums;

public static class AnyEnumExtensions
{
  extension(Any)
  {
    public static T Invalid<T>() where T : Enum
    {
      var generatedValue = Any.InstanceOf(DynamicGenHolder<T>.Instance);
      return (T)generatedValue;
    }
  }

  private static class DynamicGenHolder<T> where T : Enum
  {
    private static readonly Type EnumUnderlyingType = typeof(T).GetEnumUnderlyingType();

    private static readonly IEnumerable<object> ValidEnumValues = Enum.GetValues(typeof(T)).Cast<T>()
      .Select(s => SmartType.Cast(EnumUnderlyingType, s));
    // ReSharper disable once StaticMemberInGenericType
    public static readonly DynamicOtherThanGenerator Instance = new(EnumUnderlyingType, ValidEnumValues);
  }
}
