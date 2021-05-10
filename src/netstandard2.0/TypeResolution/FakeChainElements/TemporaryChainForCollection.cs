using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class TemporaryChainForCollection<T> : IFakeChain<T>
  {
    private readonly IResolution<T>[] _resolutions;

    public TemporaryChainForCollection(IResolution<T>[] resolutions)
    {
      _resolutions = resolutions;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      foreach (var resolution in _resolutions)
      {
        if (resolution.Applies())
        {
          request.Trace.SelectedResolution(typeof(T), resolution);
          return resolution.Apply(instanceGenerator, request);
        }
      }

      throw new ChainFailedException(typeof(T));
    }
  }
}