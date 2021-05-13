using System;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeVoidTask<T> : IResolution<T>
  {
    public bool AppliesTo(Type type)
    {
      return type == typeof(Task);
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return (T)(object)Task.CompletedTask;
    }
  }
}
