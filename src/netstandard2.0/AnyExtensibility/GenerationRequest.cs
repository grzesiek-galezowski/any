using System;

namespace TddXt.AnyExtensibility
{
  public interface GenerationRequest
  {
    GenerationTrace Trace { get; }
    NestingLimit NestingLimit { get; } //bug remove?
    T WithNextNestingLevel<T>(
      Func<T> limitNotReachedFunction, 
      Func<T> limitReachedFunction);
  }
}