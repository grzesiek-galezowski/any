using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeConcreteClass(ObjectGenerator objectGenerator) : IResolution
{
  public bool AppliesTo(Type type)
  {
    return true;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return objectGenerator.GenerateCustomizedInstance(instanceGenerator, request, type);
  }
}
