using System;
using System.Threading;
using System.Threading.Tasks;

namespace TddXt.AnyRoot.Invokable;

public static class AnyInvokableExtensions
{
  extension(Any)
  {
    /// <summary>
    /// Produces always canceled token. If you need a non-canceled token,
    /// better create one with `new CancellationToken()` and use it.
    /// </summary>
    public static CancellationToken CancellationToken()
    {
      return Any.Instance<CancellationToken>();
    }

    public static Task<T> StartedTask<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StartedTask<T>());
    }

    public static Task StartedTask()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.StartedTask());
    }

    public static Func<T> Func<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T>());
    }

    public static Func<T1, T2> Func<T1, T2>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T1, T2>());
    }

    public static Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T1, T2, T3>());
    }

    public static Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T1, T2, T3, T4>());
    }

    public static Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T1, T2, T3, T4, T5>());
    }

    public static Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Func<T1, T2, T3, T4, T5, T6>());
    }

    public static Action Action()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action());
    }

    public static Action<T> Action<T>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T>());
    }

    public static Action<T1, T2> Action<T1, T2>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T1, T2>());
    }

    public static Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T1, T2, T3>());
    }

    public static Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T1, T2, T3, T4>());
    }

    public static Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T1, T2, T3, T4, T5>());
    }

    public static Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return Any.InstanceOf(InlineGenerators.InlineGenerators.Action<T1, T2, T3, T4, T5, T6>());
    }
  }
}
