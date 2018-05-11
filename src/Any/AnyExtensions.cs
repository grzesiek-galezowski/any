using System;
using System.Linq;
using System.Net;
using System.Reflection;
using TddEbook.TddToolkit.Generators;

namespace AnyCore
{
  public static class AnyExtensions
  {
    private static readonly Type[] ValueTypes = {
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
      return gen.InstanceOf(InlineGenerators.IpAddress());
    }

    public static T From<T>(this MyGenerator gen, params T[] possibleValues)
    {
      return gen.InstanceOf(InlineGenerators.From(possibleValues));
    }

    public static DateTime DateTime(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.DateTime());
    }

    public static TimeSpan TimeSpan(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.TimeSpan());
    }

    public static bool Boolean(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Boolean());
    }

    public static object Object(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Object());
    }

    public static T Exploding<T>(this MyGenerator gen) where T : class
    {
      return gen.InstanceOf(InlineGenerators.Exploding<T>());
    }

    public static MethodInfo Method(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.MethodInfo());
    }

    public static Type Type(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Type());
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
        return gen.InstanceOf(new SimpleValueOtherThanGenerator<T>(omittedValues)); 
      }
      else
      {
        return gen.InstanceOf(new SimpleInstanceOtherThanGenerator<T>(omittedValues));
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
      return gen.InstanceOf(InlineGenerators.Exception());
    }

    public static int Port(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Port());
    }

    public static string IpString(this MyGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.IpString());
    }

  }
}