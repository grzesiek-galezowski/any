using System;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Invokable
{
  public static class AnyInvokableExtensions
  {
    public static Task NotStartedTask(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.NotStartedTask());
    }

    public static Task<T> NotStartedTask<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.NotStartedTask<T>());
    }

    public static Task<T> StartedTask<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.StartedTask<T>());
    }

    public static Task StartedTask(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.StartedTask());
    }

    public static Func<T> Func<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T>());
    }

    public static Func<T1, T2> Func<T1, T2>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2>());
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3>());
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4>());
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4, T5>());
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4, T5, T6>());
    }

    public static Action Action(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action());
    }

    public static Action<T> Action<T>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T>());
    }

    public static Action<T1, T2> Action<T1, T2>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2>());
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3>());
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4>());
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4, T5>());
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>(this BasicGenerator gen)
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4, T5, T6>());
    }
  }
}