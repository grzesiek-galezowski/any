using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

namespace TddXt.TypeResolution.FakeChainElements;

public class ResolutionOfArrays : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type.IsArray;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var elementType = type.GetElementType();
    var array = InternalInlineGenerators.GetByNameAndType(nameof(InternalInlineGenerators.Array), elementType)
      .GenerateInstance(instanceGenerator, request);
    return array;
  }
}
