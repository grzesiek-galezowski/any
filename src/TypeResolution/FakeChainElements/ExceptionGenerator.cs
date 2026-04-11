using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class ExceptionGenerator : IResolution
{

  public bool AppliesTo(Type type)
  {
    return type == typeof(Exception);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return new Exception(
      instanceGenerator.Instance<string>(request), 
      new Exception(instanceGenerator.Instance<string>(request)));
  }
}
