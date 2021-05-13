using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class ResolutionOfArrays<T> : IResolution<T>
  {
    public bool AppliesTo(Type type)
    {
      return typeof(T).IsArray;
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      //todo think it through - should it need access to InlineGenerators?
      var elementType = typeof(T).GetElementType();
      var array = InlineGenerators.GetByNameAndType(nameof(InlineGenerators.Array), elementType)
        .GenerateInstance(instanceGenerator, request);
      return (T)array;
    }
  }
}
