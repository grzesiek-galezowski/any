﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeReflection;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic;

[Serializable]
public class AllGenerator : BasicGenerator, InstanceGenerator
{
  private readonly IGenerationChain _generationChain;
  private readonly IGenerationChain _unconstrainedChain;
  private readonly FakeOrdinaryInterface _fakeOrdinaryInterfaceGenerator;

  public AllGenerator(
    IGenerationChain generationChain, 
    IGenerationChain unconstrainedChain,
    FakeOrdinaryInterface fakeOrdinaryInterfaceGenerator)
  {
    _generationChain = generationChain;
    _unconstrainedChain = unconstrainedChain;
    _fakeOrdinaryInterfaceGenerator = fakeOrdinaryInterfaceGenerator;
  }

  public T Instance<T>()
  {
    var request = DefaultGenerationRequest.WithDefaultNestingLimit();
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
    var request = DefaultGenerationRequest.WithDefaultNestingLimit(customizations);
    try
    {
      request.Trace.BeginCreatingInstanceGraphWith(typeof(T));
      return (T)_generationChain.Resolve(this, request, typeof(T));
    }
    catch (Exception e)
    {
      throw new GenerationFailedException(request, e);
    }
  }

  public T InstanceOf<T>(InlineGenerator<T> gen)
  {
    var request = DefaultGenerationRequest.WithDefaultNestingLimit();
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
    return _generationChain.Resolve(this, request, type);
  }

  public T Instance<T>(GenerationRequest request)
  {
    return (T)Instance(typeof(T), request);
  }

  public object? Dummy(GenerationRequest request, Type type)
  {
    var smartType = SmartType.For(type);

    if (AppliesTo(type))
    {
      return Apply(this, request, type);
    }

    if (type == typeof(string))
    {
      return _unconstrainedChain.Resolve(this, request, type);
    }

    var emptyCollectionInstantiation = new EmptyCollectionInstantiation();
    if (smartType.IsImplementationOfOpenGeneric(typeof(IEnumerable<>)))
    {
      return emptyCollectionInstantiation.CreateCollectionPassedAsGenericType(type);
    }

    if (smartType.IsOpenGeneric(typeof(IEnumerable<>)))
    {
      return emptyCollectionInstantiation.EmptyEnumerableOf(type.GetCollectionItemType());
    }

    if (type.IsAbstract)
    {
      return default;
    }

    if (_fakeOrdinaryInterfaceGenerator.AppliesTo(type))
    {
      return _fakeOrdinaryInterfaceGenerator.Apply(this, request, type);
    }

    return FormatterServices.GetUninitializedObject(type);
  }

  //bug unfinished refactior
  private object Apply(AllGenerator allGenerator, GenerationRequest request, Type type)
  {
    return _unconstrainedChain.Resolve(this, request, type);
  }

  private static bool AppliesTo(Type type)
  {
    return type.IsPrimitive;
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
    return (T)OtherThan(typeof(T), omittedValues?.Cast<object>()?.ToArray(), DefaultGenerationRequest.WithDefaultNestingLimit());
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
    return _generationChain.Resolve(this, request, type);
  }

  public T Instance<T>(GenerationRequest request, params GenerationCustomization[] customizations)
  {
    return (T)Instance(typeof(T), request, customizations);
  }

  public T Dummy<T>()
  {
    var request = DefaultGenerationRequest.WithDefaultNestingLimit();
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
