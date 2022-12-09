using System;

namespace TddXt.AnyExtensibility;

public interface IGenerationChain
{
  object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type);
}