using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails;

[Serializable]
public class DefaultGenerationRequest : GenerationRequest
{
  public NestingLimit NestingLimit { get; }
  public GenerationCustomization[] GenerationCustomizations { get; }
  public GenerationTrace Trace { get; }

  public DefaultGenerationRequest(
    NestingLimit nestingLimit, 
    GenerationCustomization[] generationCustomizations)
  {
    NestingLimit = nestingLimit;
    GenerationCustomizations = generationCustomizations;
    Trace = new ListBasedGeneratonTrace();
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
}

public class DeveloperError : Exception
{
  public DeveloperError(string s)
    : base(s)

  {

  }
}