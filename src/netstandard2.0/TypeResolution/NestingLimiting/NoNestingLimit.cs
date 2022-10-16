using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.NestingLimiting;

public class NoNestingLimit : NestingLimit
{
  public void AddNestingFor<T>(GenerationTrace generationTrace)
  {
  }

  public bool IsReachedFor<T>()
  {
    return false;
  }

  public void RemoveNestingFor<T>(GenerationTrace generationTrace)
  {

  }
}
