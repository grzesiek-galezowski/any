using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeConcreteClassWithNonConcreteConstructor(ObjectGenerator objectGenerator) : IResolution
{
  public bool AppliesTo(Type type)
  {
    return objectGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType(type);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return objectGenerator.GenerateCustomizedInstance(instanceGenerator, request, type);
  }
}
