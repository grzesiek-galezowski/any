using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class TemporaryChainForCollection<T> : IGenerationChain<T>
  {
    private readonly IResolution[] _resolutions;

    public TemporaryChainForCollection(IResolution[] resolutions)
    {
      _resolutions = resolutions;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      foreach (var resolution in _resolutions)
      {
        if (resolution.AppliesTo(typeof(T)))
        {
          request.Trace.SelectedResolution(typeof(T), resolution);
          return (T)resolution.Apply(instanceGenerator, request, typeof(T));
        }
      }

      throw new ChainFailedException(typeof(T));
    }
  }
}
