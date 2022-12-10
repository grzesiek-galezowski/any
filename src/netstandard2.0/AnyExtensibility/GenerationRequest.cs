using System;

namespace TddXt.AnyExtensibility;

public interface GenerationRequest
{
  GenerationTrace Trace { get; }
  GenerationCustomization[] GenerationCustomizations { get; }
  int Many { get; }

  GenerationRequest DisableLimits();

  void CustomizeCreatedValue(
    object result, 
    InstanceGenerator instanceGenerator);

  object ResolveNextNestingLevel(IGenerationChain generationChain, InstanceGenerator instanceGenerator, Type type);
  bool ReachedRecursionLimit(Type type);
}


