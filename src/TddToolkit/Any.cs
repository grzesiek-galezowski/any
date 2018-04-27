using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Net;
using System.Threading.Tasks;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.Subgenerators;
using Type = System.Type;

namespace TddEbook.TddToolkit
{
  public partial class Any
  {
    private static readonly AllGenerator gen = AllGeneratorFactory.Create();

    public static IPAddress IpAddress()
    {
      return gen.IpAddress();
    }

    public static T ValueOtherThan<T>(params T[] omittedValues)
    {
      return gen.ValueOtherThan(omittedValues);
    }

    public static T From<T>(params T[] possibleValues)
    {
      return gen.From(possibleValues);
    }

    public static DateTime DateTime()
    {
      return gen.DateTime();
    }

    public static TimeSpan TimeSpan()
    {
      return gen.TimeSpan();
    }

    public static T ValueOf<T>()
    {
      return gen.ValueOf<T>();
    }

    public static IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return gen.EmptyEnumerableOf<T>();
    }

    public static string LegalXmlTagName()
    {
      return gen.LegalXmlTagName();
    }

    public static bool Boolean()
    {
      return gen.Boolean();
    }

    public static object Object()
    {
      return gen.Object();
    }

    public static T Exploding<T>() where T : class
    {
      return gen.Exploding<T>();
    }

    public static MethodInfo Method()
    {
      return gen.Method();
    }

    public static Type Type()
    {
      return gen.Type();
    }

    public static T InstanceOf<T>()
    {
      return gen.InstanceOf<T>();
    }

    public static T Instance<T>()
    {
      return gen.Instance<T>();
    }

    public static T Dummy<T>()
    {
      return gen.Dummy<T>();
    }

#pragma warning disable CC0068 // Unused Method
#pragma warning disable S1144 // Unused private types or members should be removed
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return gen.InstanceOtherThanObjects<T>(omittedValues);
    }
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore CC0068 // Unused Method

    public static T SubstituteOf<T>() where T : class
    {
      return gen.SubstituteOf<T>();
    }

    public static T OtherThan<T>(params T[] omittedValues)
    {
      return gen.OtherThan(omittedValues);
    }

    public static Uri Uri()
    {
      return gen.Uri();
    }

    public static Guid Guid()
    {
      return gen.Guid();
    }

    public static string UrlString()
    {
      return gen.UrlString();
    }

    public static Exception Exception()
    {
      return gen.Exception();
    }

    public static int Port()
    {
      return gen.Port();
    }

    public static string Ip()
    {
      return gen.Ip();
    }

    public static object Instance(Type type)
    {
      return gen.Instance(type);
    }

    public static IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return gen.EnumerableWith(included);
    }

    public static Task NotStartedTask()
    {
      return gen.NotStartedTask();
    }

    public static Task<T> NotStartedTask<T>()
    {
      return gen.NotStartedTask<T>();
    }


    public static Task<T> StartedTask<T>()
    {
      return gen.StartedTask<T>();
    }

    public static Func<T> Func<T>()
    {
      return gen.Func<T>();
    }
    public static Func<T1, T2> Func<T1, T2>()
    {
      return gen.Func<T1, T2>();
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return gen.Func<T1, T2, T3>();
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return gen.Func<T1, T2, T3, T4>();
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return gen.Func<T1, T2, T3, T4, T5>();
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return gen.Func<T1, T2, T3, T4, T5, T6>();
    }

    public static Action Action()
    {
      return gen.Action();
    }

    public static Action<T> Action<T>()
    {
      return gen.Action<T>();
    }
    public static Action<T1, T2> Action<T1, T2>()
    {
      return gen.Action<T1, T2>();
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return gen.Action<T1, T2, T3>();
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return gen.Action<T1, T2, T3, T4>();
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return gen.Action<T1, T2, T3, T4, T5>();
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return gen.Action<T1, T2, T3, T4, T5, T6>();
    }
  }


}

