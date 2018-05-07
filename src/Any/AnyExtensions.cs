using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Reflection;
using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyExtensions
  {
    private static readonly Type[] ValueTypes = new[]
    {
      typeof(byte),
      typeof(short),
      typeof(ushort),
      typeof(decimal),
      typeof(int),
      typeof(uint),
      typeof(long),
      typeof(ulong),
      typeof(float),
      typeof(double),
      typeof(bool),
      typeof(sbyte),
      typeof(char),
      typeof(object),
      typeof(string)
    };

    public static IPAddress IpAddress(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<IPAddress>();
    }

    public static T From<T>(this MyGenerator gen, params T[] possibleValues)
    {
      return gen.InstanceOf(InlineGenerators.From(possibleValues));
    }

    public static DateTime DateTime(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<DateTime>();
    }

    public static TimeSpan TimeSpan(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<TimeSpan>();
    }

    public static bool Boolean(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<bool>();
    }

    public static object Object(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<object>();
    }

    public static T Exploding<T>(this MyGenerator gen) where T : class
    {
      return gen.AllGenerator.Exploding<T>();
    }

    public static MethodInfo Method(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<MethodInfo>();
    }

    public static Type Type(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<Type>();
    }

    public static T Instance<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Instance<T>();
    }

    public static T Dummy<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Dummy<T>();
    }

    public static T Substitute<T>(this MyGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Substitute<T>());
    }

    public static T OtherThan<T>(this MyGenerator gen, params T[] omittedValues)
    {
      if (ValueTypes.Contains(typeof(T)))
      {
        return gen.AllGenerator.ValueOtherThan(omittedValues); 
      }
      else
      {
        return gen.AllGenerator.OtherThan(omittedValues);
      }
    }

    public static Uri Uri(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Uri());
    }

    public static Guid Guid(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Guid());
    }

    public static string UrlString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.UrlString());
    }

    public static Exception Exception(this MyGenerator gen)
    {
      return gen.AllGenerator.Value<Exception>();
    }

    public static int Port(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Port());
    }

    public static string Ip(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.IpString());
    }

    public static object Instance(this MyGenerator gen, Type type)
    {
      return gen.AllGenerator.Instance(type);
    }
  }
}