using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public interface IFakeChain<out T>
  {
    T Resolve(InstanceGenerator instanceGenerator);
  }

  public class FakeChain<T> : IFakeChain<T>
  {
    private readonly IChainElement<T> _chainHead;
    //bug pass CollectionGenerator here, but make non-static


    public FakeChain(IChainElement<T> chainHead)
    {
      _chainHead = chainHead;
    }

    public T Resolve(InstanceGenerator instanceGenerator)
    {
      return _chainHead.Resolve(instanceGenerator);
    }
  }
}