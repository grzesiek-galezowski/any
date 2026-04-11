using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.NestingLimiting;

public class NoNestingLimit : NestingLimit
{
  public void AddNestingFor(Type type, GenerationTrace generationTrace)
  {
  }

  public bool IsReachedFor(Type type)
  {
    return false;
  }

  public void RemoveNestingFor(Type type, GenerationTrace generationTrace)
  {

  }
}
