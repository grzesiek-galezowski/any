using System;
using System.Linq;
using System.Reflection;

namespace Generators
{
  public static class TypeExtensions
  {
    public static Type GetCollectionItemType(this Type type)
    {
      if (type.GetTypeInfo().IsArray)
        return type.GetElementType();
      if (type.GetTypeInfo().IsGenericType)
        return type.GetGenericArguments().Single();
      throw new ArgumentException(
        "The argument is not a valid collection type.", "type");
    }
  }
}