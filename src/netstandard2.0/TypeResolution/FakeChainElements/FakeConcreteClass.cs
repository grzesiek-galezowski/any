using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeConcreteClass : IResolution
{
  private readonly ObjectGenerator _objectGenerator;

  public FakeConcreteClass(ObjectGenerator objectGenerator)
  {
    _objectGenerator = objectGenerator;
  }

  public bool AppliesTo(Type type)
  {
    return true;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _objectGenerator.GenerateCustomizedInstance(instanceGenerator, request, type);
  }
}
