using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution;
using TddXt.TypeResolution.NestingLimiting;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails;

[Serializable]
public class DefaultGenerationRequest : GenerationRequest
{
  public NestingLimit NestingLimit { get; }
  public GenerationCustomization[] GenerationCustomizations { get; }
  public GenerationTrace Trace { get; }

  internal DefaultGenerationRequest(
    NestingLimit nestingLimit, 
    GenerationCustomization[] generationCustomizations, 
    GenerationTrace trace)
  {
    NestingLimit = nestingLimit;
    GenerationCustomizations = generationCustomizations;
    Trace = trace;
  }

  public T WithNextNestingLevel<T>(Func<T> limitNotReachedFunction,
    Func<T> limitReachedFunction)
  {
    try
    {
      NestingLimit.AddNestingFor<T>(Trace);
      if (!NestingLimit.IsReachedFor<T>())
      {
        return limitNotReachedFunction.Invoke();
      }
      else
      {
        return limitReachedFunction.Invoke();
      }
    }
    finally
    {
      NestingLimit.RemoveNestingFor<T>(Trace);
    }
  }

  public static GenerationRequest WithDefaultNestingLimit(params GenerationCustomization[] customizations)
  {
    return new DefaultGenerationRequest(
      GlobalNestingLimit.Of(5), 
      customizations, 
      new ListBasedGeneratonTrace());
  }

  public GenerationRequest DisableNestingLimit() 
    => new DefaultGenerationRequest(
      new NoNestingLimit(), 
      GenerationCustomizations, 
      Trace);
}

public class DeveloperError : Exception
{
  public DeveloperError(string s)
    : base(s)

  {

  }
}
