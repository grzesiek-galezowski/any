using System;
using System.Linq;
using Core.Maybe;
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
    return request.GenerationCustomizations.Where(c => c.AppliesTo(type)).FirstMaybe()
      .SelectOrElse(
        c => c.Generate(type, instanceGenerator, request),
        () => _next.Resolve(instanceGenerator, request, type));
  }
}
