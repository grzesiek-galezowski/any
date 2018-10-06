using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IChainElement<out T>
  {
    T Resolve(InstanceGenerator instanceGenerator, GenerationTrace trace);
  }

  public class ChainElement<T> : IChainElement<T>
  {
    private readonly IChainElement<T> _next;
    private readonly IResolution<T> _resolution;

    public ChainElement(IResolution<T> resolution, IChainElement<T> next)
    {
      _next = next;
      _resolution = resolution;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      if (_resolution.Applies())
      {
        trace.SelectedResolution(typeof(T), _resolution);
        return _resolution.Apply(instanceGenerator, trace);
      }
      else
      {
        return _next.Resolve(instanceGenerator, trace);
      }
    }
  }
}