using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyExtentions
  {
    public static IPAddress IpAddress(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<IPAddress>();
    }

    public static T ValueOtherThan<T>(this MyGenerator gen, params T[] omittedValues)
    {
      return gen.AllGenerator.ValueOtherThan(omittedValues);
    }

    public static T From<T>(this MyGenerator gen, params T[] possibleValues)
    {
      return gen.InstanceOf(InlineGenerators.From(possibleValues));
    }

    public static DateTime DateTime(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<DateTime>();
    }

    public static TimeSpan TimeSpan(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<TimeSpan>();
    }

    public static T ValueOf<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<T>();
    }

    //todo needed???
    public static IEnumerable<T> EmptyEnumerableOf<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.EmptyEnumerableOf<T>();
    }

    public static bool Boolean(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<bool>();
    }

    public static object Object(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<object>();
    }

    public static T Exploding<T>(this MyGenerator gen) where T : class
    {
      return gen.AllGenerator.Exploding<T>();
    }

    public static MethodInfo Method(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<MethodInfo>();
    }

    public static Type Type(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<Type>();
    }

    public static T InstanceOf<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.InstanceOf<T>();
    }

    public static T Instance<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Instance<T>();
    }

    public static T Dummy<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Dummy<T>();
    }

#pragma warning disable CC0068 // Unused Method
#pragma warning disable S1144 // Unused private types or members should be removed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static T InstanceOtherThanObjects<T>(this MyGenerator gen, params object[] omittedValues)
    {
      return gen.AllGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore CC0068 // Unused Method

    public static T SubstituteOf<T>(this MyGenerator gen) where T : class
    {
      return gen.AllGenerator.SubstituteOf<T>();
    }

    public static T OtherThan<T>(this MyGenerator gen, params T[] omittedValues)
    {
      return gen.AllGenerator.OtherThan(omittedValues);
    }

    public static Uri Uri(this MyGenerator gen)
    {
      return gen.AllGenerator.Uri();
    }

    public static Guid Guid(this MyGenerator gen)
    {
      return gen.AllGenerator.Guid();
    }

    public static string UrlString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UrlString());
    }

    public static Exception Exception(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<Exception>();
    }

    public static int Port(this MyGenerator gen)
    {
      return gen.AllGenerator.Port();
    }

    public static string Ip(this MyGenerator gen)
    {
      return gen.AllGenerator.Ip();
    }

    public static object Instance(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.Instance(type);
    }
  }
}