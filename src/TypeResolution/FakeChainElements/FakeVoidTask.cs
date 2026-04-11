using System;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

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