using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

//bug cannot be moved (yet) to where other resolutions are due to dependency on
//bug InlineGenerators
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
