using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

public class CustomizationSupportingChain : IGenerationChain
{
  private readonly IGenerationChain _next;

  public CustomizationSupportingChain(IGenerationChain next)
  {
    _next = next;
  }

  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var customization in request.GenerationCustomizations)
    {
      if (customization.AppliesTo(type))
      {
        return customization.Generate(type, instanceGenerator, request);
      }
    }

    return _next.Resolve(instanceGenerator, request, type);
  }
}
