using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Generic
{
  public class DummyGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Dummy<T>(new GenerationTrace());
    }
  }
}