using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.NestingLimiting;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

[Serializable]
public class DefaultGenerationRequest : GenerationRequest
{
  public NestingLimit NestingLimit { get; }
  public GenerationCustomization[] GenerationCustomizations { get; }
  private GeneratedObjectCustomization[] GeneratedObjectCustomizations { get; }
  public GenerationTrace Trace { get; }

  internal DefaultGenerationRequest(
    NestingLimit nestingLimit,
    GenerationCustomization[] generationCustomizations,
    GeneratedObjectCustomization[] generatedObjectCustomizations,
    GenerationTrace trace)
  {
    NestingLimit = nestingLimit;
    GenerationCustomizations = generationCustomizations;
    GeneratedObjectCustomizations = generatedObjectCustomizations;
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
      new GeneratedObjectCustomization[]
      {
        new FillFieldsCustomization(),
        new FillPropertiesCustomization()
      },
      new ListBasedGeneratonTrace());
  }

  public GenerationRequest DisableNestingLimit() 
    => new DefaultGenerationRequest(
      new NoNestingLimit(), 
      GenerationCustomizations, 
      GeneratedObjectCustomizations, 
      Trace);

  public void CustomizeCreatedValue(
    object result, 
    InstanceGenerator instanceGenerator)
  {
    foreach (var customization in GeneratedObjectCustomizations)
    {
      customization.ApplyTo(result, instanceGenerator, this);
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
