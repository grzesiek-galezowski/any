using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeConcreteClassWithNonConcreteConstructor : IResolution
{
  private readonly ObjectGenerator _objectGenerator;

  public FakeConcreteClassWithNonConcreteConstructor(ObjectGenerator objectGenerator)
  {
    _objectGenerator = objectGenerator;
  }

  public bool AppliesTo(Type type)
  {
    return _objectGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType(type);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _objectGenerator.GenerateCustomizedInstance(instanceGenerator, request, type);
  }
}