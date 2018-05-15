using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
{
  public interface IResolution<out T>
  {
    bool Applies();
    T Apply(InstanceGenerator instanceGenerator);
  }
}