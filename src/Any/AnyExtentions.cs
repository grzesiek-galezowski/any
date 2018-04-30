using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace AnyCore
{
  public static class AnyExtentions
  {
    public static IPAddress IpAddress(this MyGenerator gen)
    {
      return gen.AllGenerator.IpAddress();
    }

    public static T ValueOtherThan<T>(this MyGenerator gen, params T[] omittedValues)
    {
      return gen.AllGenerator.ValueOtherThan(omittedValues);
    }

    public static T From<T>(this MyGenerator gen, params T[] possibleValues)
    {
      return gen.AllGenerator.From(possibleValues);
    }

    public static DateTime DateTime(this MyGenerator gen)
    {
      return gen.AllGenerator.DateTime();
    }

    public static TimeSpan TimeSpan(this MyGenerator gen)
    {
      return gen.AllGenerator.TimeSpan();
    }

    public static T ValueOf<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.ValueOf<T>();
    }

    public static IEnumerable<T> EmptyEnumerableOf<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.EmptyEnumerableOf<T>();
    }

    public static string LegalXmlTagName(this MyGenerator gen)
    {
      return gen.AllGenerator.LegalXmlTagName();
    }

    public static bool Boolean(this MyGenerator gen)
    {
      return gen.AllGenerator.Boolean();
    }

    public static object Object(this MyGenerator gen)
    {
      return gen.AllGenerator.Object();
    }

    public static T Exploding<T>(this MyGenerator gen) where T : class
    {
      return gen.AllGenerator.Exploding<T>();
    }

    public static MethodInfo Method(this MyGenerator gen)
    {
      return gen.AllGenerator.Method();
    }

    public static Type Type(this MyGenerator gen)
    {
      return gen.AllGenerator.Type();
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
      return gen.AllGenerator.UrlString();
    }

    public static Exception Exception(this MyGenerator gen)
    {
      return gen.AllGenerator.Exception();
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

    public static IEnumerable<T> EnumerableWith<T>(this MyGenerator gen, IEnumerable<T> included)
    {
      return gen.AllGenerator.EnumerableWith(included);
    }

    public static Task NotStartedTask(this MyGenerator gen)
    {
      return gen.AllGenerator.NotStartedTask();
    }

    public static Task<T> NotStartedTask<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.NotStartedTask<T>();
    }


    public static Task<T> StartedTask<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.StartedTask<T>();
    }

    public static Func<T> Func<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T>();
    }
    public static Func<T1, T2> Func<T1, T2>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T1, T2>();
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T1, T2, T3>();
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T1, T2, T3, T4>();
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T1, T2, T3, T4, T5>();
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>(this MyGenerator gen)
    {
      return gen.AllGenerator.Func<T1, T2, T3, T4, T5, T6>();
    }

    public static Action Action(this MyGenerator gen)
    {
      return gen.AllGenerator.Action();
    }

    public static Action<T> Action<T>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T>();
    }
    public static Action<T1, T2> Action<T1, T2>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T1, T2>();
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T1, T2, T3>();
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T1, T2, T3, T4>();
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T1, T2, T3, T4, T5>();
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>(this MyGenerator gen)
    {
      return gen.AllGenerator.Action<T1, T2, T3, T4, T5, T6>();
    }
  }
}