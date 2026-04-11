using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution.ResolutionChaining;

public class GeneratorsBasedChain(IResolution[] resolutions) : IGenerationChain
{
  public object? Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var resolution in resolutions)
    {
      if (resolution.AppliesTo(type))
      {
        request.Trace.SelectedResolution(type, resolution);
        return resolution.Apply(instanceGenerator, request, type);
      }
    }

    throw new ChainFailedException(type);
  }
}
