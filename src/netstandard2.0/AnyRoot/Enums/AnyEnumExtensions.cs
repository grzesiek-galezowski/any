using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.AnyRoot.Enums
{
  public static class AnyEnumExtensions
  {
    public static T Invalid<T>(this BasicGenerator gen) where T : Enum
    {
      var generatedValue = gen.InstanceOf(DynamicGenHolder<T>.Instance);
      return (T)generatedValue;
    }

    private static class DynamicGenHolder<T>
    {
      private static readonly Type EnumUnderlyingType = typeof(T).GetEnumUnderlyingType();
      private static readonly IEnumerable<object> ValidEnumValues = ((IEnumerable<T>) Enum.GetValues(typeof(T)))
        .Select(s => SmartType.Cast(EnumUnderlyingType, s));
      // ReSharper disable once StaticMemberInGenericType
      public static readonly DynamicOtherThanGenerator Instance = new(EnumUnderlyingType, ValidEnumValues);
    }
  }
}
