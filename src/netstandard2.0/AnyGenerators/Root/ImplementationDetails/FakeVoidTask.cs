using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeVoidTask<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof(T) == typeof(Task);
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return (T)(object)Task.CompletedTask;
    }
  }
}