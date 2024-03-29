﻿using System;
using System.Reflection;

namespace TddXt.TypeReflection;

public static class TypeOfType
{
  public static bool Is<T>()
  {
    return Is(typeof(T));
  }

  public static bool Is(Type t)
  {
    return t.FullName == typeof(Type).FullName || IsTypeOfTypeWithinBaseHierarchyOf(t);
  }

  private static bool IsTypeOfTypeWithinBaseHierarchyOf(Type type)
  {
    var baseType = type.GetTypeInfo().BaseType;
    if (baseType == null)
    {
      return false;
    }
    else if (baseType.FullName == typeof(Type).FullName)
    {
      return true;
    }
    else
    {
      return IsTypeOfTypeWithinBaseHierarchyOf(baseType);
    }

  }
}