using System;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyEnumExtensions
  {
    public static T Of<T>(this MyGenerator gen) where T : struct, IConvertible
    {
      return gen.AllGenerator.Of<T>();
    }

    /// <summary>
    /// Generates enum member
    /// </summary>
    /// <typeparam name="T">MUST BE AN ENUM. FOR NORMAL VALUES, USE Any.OtherThan()</typeparam>
    /// <param name="gen"></param>
    /// <param name="excludedValues"></param>
    /// <returns></returns>
    public static T Besides<[MustBeAnEnum] T>(this MyGenerator gen, [MustBeAnEnum] params T[] excludedValues)
      where T : struct, IConvertible
    {
      return gen.AllGenerator.Besides(excludedValues);
    }
  }
}