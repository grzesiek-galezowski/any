using System;

namespace TddXt.AnyExtensibility;

public interface GenerationRequest
{
  GenerationTrace Trace { get; }
  GenerationCustomization[] GenerationCustomizations { get; }

  object WithNextNestingLevel(Type generatedType, Func<object> limitNotReachedFunction,
    Func<object> limitReachedFunction);

  GenerationRequest DisableNestingLimit();

  void CustomizeCreatedValue(
    object result, 
    InstanceGenerator instanceGenerator);
}


