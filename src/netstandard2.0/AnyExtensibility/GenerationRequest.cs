using System;

namespace TddXt.AnyExtensibility;

public interface GenerationRequest
{
  GenerationTrace Trace { get; }
  GenerationCustomization[] GenerationCustomizations { get; }

  T WithNextNestingLevel<T>(
    Func<T> limitNotReachedFunction,
    Func<T> limitReachedFunction);

  GenerationRequest DisableNestingLimit();
}
