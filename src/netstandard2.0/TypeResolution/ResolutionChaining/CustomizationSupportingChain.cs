using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionChaining;

public class CustomizationSupportingChain(IGenerationChain next) : IGenerationChain
{
  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var customization in request.GenerationCustomizations)
    {
      if (customization.AppliesTo(type))
      {
        return customization.Generate(type, instanceGenerator, request);
      }
    }

    return next.Resolve(instanceGenerator, request, type);
  }
}
