using TddXt.AnyExtensibility;

namespace TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply(InstanceGenerator instanceGenerator);
  }
}