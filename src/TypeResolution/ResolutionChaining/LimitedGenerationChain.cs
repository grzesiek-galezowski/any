using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.ResolutionChaining;

public class LimitedGenerationChain(IGenerationChain generationChain) : IGenerationChain
{
  public object? Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return request.ResolveNextNestingLevel(generationChain, instanceGenerator, type);
  }
}
