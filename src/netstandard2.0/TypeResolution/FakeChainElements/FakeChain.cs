using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T>
  {
    T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request);
  }

  public class FakeChain<T> : IFakeChain<T>
  {
    private readonly IChainElement<T> _chainHead;

    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return _chainHead.Resolve(instanceGenerator, request);
    }
  }
}