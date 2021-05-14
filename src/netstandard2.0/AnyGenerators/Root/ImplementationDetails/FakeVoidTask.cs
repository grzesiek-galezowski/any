using System;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeVoidTask : IResolution
  {
    public bool AppliesTo(Type type)
    {
      return type == typeof(Task);
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return Task.CompletedTask;
    }
  }
}
