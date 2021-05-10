using System;

namespace TddXt.AnyExtensibility
{
  public interface GenerationRequest
  {
    GenerationTrace Trace { get; }
    NestingLimit NestingLimit { get; } //bug remove?
    GenerationCustomization[] GenerationCustomizations { get; }

    T WithNextNestingLevel<T>(
      Func<T> limitNotReachedFunction,
      Func<T> limitReachedFunction);
  }
}