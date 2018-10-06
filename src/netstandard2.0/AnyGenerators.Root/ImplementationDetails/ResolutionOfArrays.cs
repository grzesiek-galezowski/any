using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class ResolutionOfArrays<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof (T).IsArray;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      //todo think it through - should it need access to InlineGenerators?
      var elementType = typeof (T).GetElementType();
      var array = InlineGenerators.GetByNameAndType(nameof(InlineGenerators.Array), elementType)
        .GenerateInstance(instanceGenerator, trace);
      return (T)array;
    }
  }
}