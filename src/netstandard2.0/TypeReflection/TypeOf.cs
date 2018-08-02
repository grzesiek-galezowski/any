using System;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeReflection
{
  public static class TypeOf<T>
  {
    private static readonly IType Type;

    static TypeOf()
    {
      Type = SmartType.For(typeof (T));
    }

    public static bool HasParameterlessConstructor()
    {
      return Type.HasPublicParameterlessConstructor();
    }

    public static bool IsImplementationOfOpenGeneric(Type openGenericType)
    {
      return Type.IsImplementationOfOpenGeneric(openGenericType);
    }

    public static bool IsConcrete()
    {
      return Type.IsConcrete();
    }

    public static bool Is<T1>()
    {
      return typeof (T) == typeof (T1);
    }

    public static bool IsOpenGeneric(Type type)
    {
      return Type.IsOpenGeneric(type);
    }
  }
}