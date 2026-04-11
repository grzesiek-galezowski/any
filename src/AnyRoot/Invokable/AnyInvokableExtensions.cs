using System;
using System.Threading;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root;

namespace TddXt.AnyRoot.Invokable;

public static class AnyInvokableExtensions
{
  extension(BasicGenerator gen)
  {
    /// <summary>
    /// Produces always canceled token. If you need a non-canceled token,
    /// better create one with `new CancellationToken()` and use it.
    /// </summary>
    public CancellationToken CancellationToken()
    {
      return gen.Instance<CancellationToken>();
    }

    public Task<T> StartedTask<T>()
    {
      return gen.InstanceOf(InlineGenerators.StartedTask<T>());
    }

    public Task StartedTask()
    {
      return gen.InstanceOf(InlineGenerators.StartedTask());
    }

    public Func<T> Func<T>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T>());
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2>());
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3>());
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4>());
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4, T5>());
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return gen.InstanceOf(InlineGenerators.Func<T1, T2, T3, T4, T5, T6>());
    }

    public Action Action()
    {
      return gen.InstanceOf(InlineGenerators.Action());
    }

    public Action<T> Action<T>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T>());
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2>());
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3>());
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4>());
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4, T5>());
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return gen.InstanceOf(InlineGenerators.Action<T1, T2, T3, T4, T5, T6>());
    }
  }
}
