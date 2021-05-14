using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class TemporaryChainForCollection : IGenerationChain
  {
    private readonly Type _type;
    private readonly IResolution[] _resolutions;

    public TemporaryChainForCollection(Type type, IResolution[] resolutions)
    {
      _type = type;
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
}
