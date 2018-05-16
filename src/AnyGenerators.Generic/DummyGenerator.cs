using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace Generators.Inline
{
  public class DummyGenerator<T> : InlineGenerator<T>
  {
    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return instanceGenerator.Dummy<T>();
    }
  }
}