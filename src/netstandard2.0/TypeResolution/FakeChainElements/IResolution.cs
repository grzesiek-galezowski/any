using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace);
  }
}