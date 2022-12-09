using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

[Serializable]
public class DefaultGenerationRequest : GenerationRequest
{
  private ImmutableList<Type> _resolutionPath;
  private int _currentNesting;
  private int _maxNesting;
  private int _maxRecursion;
  private readonly int _many;
  private GeneratedObjectCustomization[] GeneratedObjectCustomizations { get; }

  //public NestingLimit NestingLimit { get; }
  public GenerationCustomization[] GenerationCustomizations { get; }
  public int Many => _many;
  public GenerationTrace Trace { get; }

  internal DefaultGenerationRequest(
    //NestingLimit nestingLimit,
    GenerationCustomization[] generationCustomizations,
    GeneratedObjectCustomization[] generatedObjectCustomizations,
    GenerationTrace trace,
    int many,
    int currentNesting,
    int maxNesting,
    int maxRecursion,
    ImmutableList<Type> resolutionPath)
  {
    //NestingLimit = nestingLimit;
    _currentNesting = currentNesting;
    _maxNesting = maxNesting;
    _maxRecursion = maxRecursion;
    _resolutionPath = resolutionPath;
    GenerationCustomizations = generationCustomizations;
    GeneratedObjectCustomizations = generatedObjectCustomizations;
    Trace = trace;
    _many = many;
  }

  public static GenerationRequest WithDefaultLimits(params GenerationCustomization[] customizations)
  {
    return new DefaultGenerationRequest(
      //GlobalNestingLimit.Of(5), 
      customizations, 
      new GeneratedObjectCustomization[]
      {
        new FillFieldsCustomization(),
        new FillPropertiesCustomization()
      },
      new ListBasedGeneratonTrace(), 
      Configuration.Many, 
      1, 
      5, 
      5, 
      ImmutableList<Type>.Empty);
  }

  public GenerationRequest DisableLimits()
  {
    return new DefaultGenerationRequest(
      GenerationCustomizations,
      GeneratedObjectCustomizations,
      Trace, 
      Configuration.Many, 
      0, 
      int.MaxValue, 
      int.MaxValue, 
      ImmutableList<Type>.Empty);
  }

  public void CustomizeCreatedValue(
    object result, 
    InstanceGenerator instanceGenerator)
  {
    foreach (var customization in GeneratedObjectCustomizations)
    {
      customization.ApplyTo(result, instanceGenerator, this);
    }
  }

  public object ResolveNextNestingLevel(
    IGenerationChain generationChain,
    InstanceGenerator instanceGenerator,
    Type type)
  {
    Trace.AddNestingAndCheckWith(_currentNesting, type);
    try
    {
      if (_resolutionPath.Count(t => t == type) >= _maxRecursion) //>= because number of elements == path + current type
      {
        try
        {
          Trace.RecursionLimitReachedTryingDummy();
          return instanceGenerator.Dummy(this.WithIncreasedNesting(type), type)!; //TODO
        }
        catch (TargetInvocationException e)
        {
          return default!;
        }
        catch (MemberAccessException e)
        {
          return default!;
        }
        catch (ArgumentException e)
        {
          return default!;
        }
      }
      else
      {
        return generationChain.Resolve(
          instanceGenerator,
          this.WithIncreasedNesting(type),
          type);
      }
    }
    finally
    {
      Trace.RemoveNestingAndCheckWith(_currentNesting, type);
    }
  }

  private GenerationRequest WithIncreasedNesting(Type type)
  {
    var nextNesting = _currentNesting+1;
    int many;
    if (_currentNesting > _maxNesting)
    {
      many = 0;
    }
    else
    {
      many = _many;
    }

    return new DefaultGenerationRequest(
      GenerationCustomizations, 
      GeneratedObjectCustomizations, 
      Trace, 
      many, //bug
      nextNesting, 
      _maxNesting, 
      _maxRecursion,
      _resolutionPath.Add(type));
  }
}

public class DeveloperError : Exception
{
  public DeveloperError(string s)
    : base(s)

  {

  }
}
