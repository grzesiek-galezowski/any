using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

namespace TddXt.TypeResolution;

[Serializable]
public class AllGenerator(
  IGenerationChain generationChain,
  IGenerationChain dummyInstanceChain)
  : BasicGenerator, InstanceGenerator
{
  private readonly IGenerationChain _generationChain = generationChain;
  private readonly IGenerationChain _dummyInstanceChain = dummyInstanceChain;

  public T Instance<T>()
  {
    var request = DefaultGenerationRequest.WithDefaultLimits();
    try
    {
      request.Trace.BeginCreatingInstanceGraphWith(typeof(T));
      return Instance<T>(request);
    }
    catch (Exception e)
    {
      throw new GenerationFailedException(request, e);
    }
  }

  public T Instance<T>(params GenerationCustomization[] customizations)
  {
    var request = DefaultGenerationRequest.WithDefaultLimits(customizations);
    try
    {
      request.Trace.BeginCreatingInstanceGraphWith(typeof(T));
      return (T)_generationChain.Resolve(this, request, typeof(T))!;
    }
    catch (Exception e)
    {
      throw new GenerationFailedException(request, e);
    }
  }

  public T InstanceOf<T>(InlineGenerator<T> gen)
  {
    var request = DefaultGenerationRequest.WithDefaultLimits();
    try
    {
      request.Trace.BeginCreatingInstanceGraphWithInlineGenerator(typeof(T), gen);
      return gen.GenerateInstance(this, request);
    }
    catch (Exception e)
    {
      throw new GenerationFailedException(request, e);
    }
  }

  public object Instance(Type type, GenerationRequest request)
  {
    return _generationChain.Resolve(this, request, type)!;
  }

  public T Instance<T>(GenerationRequest request)
  {
    return (T)Instance(typeof(T), request);
  }

  public object? Dummy(GenerationRequest request, Type type)
  {
    return _dummyInstanceChain.Resolve(this, request, type);
  }

  public T Dummy<T>(GenerationRequest request)
  {
    var dummy = Dummy(request, typeof(T));
    if (dummy is T result)
    {
      return result;
    }

    throw new Exception("Error while generating a dummy instance. Generated dummy is not of type " + typeof(T));
  }

  public T OtherThan<T>(params T[]? omittedValues)
  {
    return (T)OtherThan(typeof(T), omittedValues?.Cast<object>()?.ToArray() ?? [], DefaultGenerationRequest.WithDefaultLimits());
  }

  public object OtherThan(Type type, object[] skippedValues, GenerationRequest request)
  {
    if (type.IsEnum)
    {
      if (TryingToSkipAllValuesOf(type, skippedValues))
      {
        throw new Exception("skipped values consist of all the enum members. No value left to generate");
      }
    }

    object currentValue;
    do
    {
      currentValue = Instance(type, request);
    } while (skippedValues.Contains(currentValue));

    return currentValue;
  }

  public object Instance(Type type, GenerationRequest request, params GenerationCustomization[] customizations)
  {
    return _generationChain.Resolve(this, request, type)!;
  }

  public T Instance<T>(GenerationRequest request, params GenerationCustomization[] customizations)
  {
    return (T)Instance(typeof(T), request, customizations);
  }

  public T Dummy<T>()
  {
    var request = DefaultGenerationRequest.WithDefaultLimits();
    try
    {
      request.Trace.BeginCreatingDummyInstanceOf(typeof(T));
      return Dummy<T>(request);
    }
    catch (Exception e)
    {
      throw new GenerationFailedException(request, e);
    }
  }

  private static bool TryingToSkipAllValuesOf(Type type, IEnumerable<object> skippedValues)
  {
    return Enum.GetValues(type)
      .Cast<object>()
      .ToList()
      .OrderBy(v => v)
      .SequenceEqual(skippedValues.OrderBy(v => v));
  }
}
