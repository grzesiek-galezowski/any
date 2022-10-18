using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution.ResolutionChaining;

public class GenereatorsBasedChain : IGenerationChain
{
  private readonly IResolution[] _resolutions;

  public GenereatorsBasedChain(IResolution[] resolutions)
  {
    _resolutions = resolutions;
  }

  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var resolution in _resolutions)
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
