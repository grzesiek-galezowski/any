using System;
using System.Threading.Tasks;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class InvokableGenerator
  {
    public Task NotStartedTask()
    {
      return new Task(() => Task.Delay(1).Wait());
    }

    public Task<T> NotStartedTask<T>(InstanceGenerator instanceGenerator)
    {
      return new Task<T>(instanceGenerator.Instance<T>);
    }

    public Task<T> StartedTask<T>(InstanceGenerator genericGenerator)
    {
      return Task.FromResult(genericGenerator.Instance<T>());
    }

    public Func<T> Func<T>(InstanceGenerator instanceGenerator)
    {

      return instanceGenerator.Instance<Func<T>>();
    }

    public Func<T1, T2> Func<T1, T2>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2>>();
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3>>();
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4>>();
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4, T5>>();
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public Action Action(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action>();
    }

    public Action<T> Action<T>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T>>();
    }

    public Action<T1, T2> Action<T1, T2>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2>>();
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3>>();
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4>>();
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4, T5>>();
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Instance<Action<T1, T2, T3, T4, T5, T6>>();
    }
  }
}