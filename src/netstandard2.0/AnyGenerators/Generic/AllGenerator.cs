using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FluentAssertions;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeReflection;
using TddXt.TypeResolution;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class AllGenerator : CustomizableInstanceGenerator, BasicGenerator
  {
    private readonly IGenerationChain _generationChain;
    private readonly IGenerationChain _unconstrainedChain;
    private readonly FakeOrdinaryInterface _fakeOrdinaryInterfaceGenerator;

    [NonSerialized] private readonly ValueGenerator _valueGenerator;

    public AllGenerator(
      ValueGenerator valueGenerator, 
      IGenerationChain generationChain, 
      IGenerationChain unconstrainedChain,
      FakeOrdinaryInterface fakeOrdinaryInterfaceGenerator)
    {
      _valueGenerator = valueGenerator;
      _generationChain = generationChain;
      _unconstrainedChain = unconstrainedChain;
      _fakeOrdinaryInterfaceGenerator = fakeOrdinaryInterfaceGenerator;
    }

    public T Instance<T>()
    {
      var request = CreateRequest();
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
      var request = CreateRequest(customizations);
      try
      {
        request.Trace.BeginCreatingInstanceGraphWith(typeof(T));
        return (T)_generationChain.Resolve(CreateCustomizedInstanceGenerator(), request, typeof(T));
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(request, e);
      }
    }

    private InstanceGenerator CreateCustomizedInstanceGenerator()
    {
      return new CustomizedGenerator(this);
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      var request = CreateRequest();
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

    public T ValueOtherThan<T>(GenerationRequest request, params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(this, request, omittedValues);
    }

    public T Value<T>(GenerationRequest request)
    {
      return (T)Value(typeof(T), request);
    }

    public object Value(Type type, GenerationRequest request)
    {
      return _valueGenerator.Value(type, this, request);
    }

    public T Value<T>(GenerationRequest request, GenerationCustomization[] customizations)
    {
      return _valueGenerator.Value<T>(
        CreateCustomizedInstanceGenerator(), request);
    }

    public T Value<T>(T seed, GenerationRequest request)
    {
      request.Trace.GeneratingSeededValue(typeof(T), seed);
      return _valueGenerator.Value(this, request, seed);
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

      if (type.IsPrimitive)
      {
        return _unconstrainedChain.Resolve(this, request, type);
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

    public T Dummy<T>(GenerationRequest request)
    {
      var dummy = Dummy(request, typeof(T));
      if (dummy is T result)
      {
        return result;
      }

      throw new Exception($"Error while generating a dummy instance. Generated dummy is {dummy}, not " + typeof(T));
    }
    
    public T OtherThan<T>(params T[]? omittedValues)
    {
      return (T)OtherThan(typeof(T), omittedValues?.Cast<object>()?.ToArray(), CreateRequest());
    }

    public object OtherThan(Type type, object[] omittedValues, GenerationRequest request)
    {
      if (type.IsEnum)
      {
        Enum.GetValues(type).Should().NotBeEquivalentTo(omittedValues,
          "skipped values consist of all the enum members. No value left to generate");
      }

      object currentValue;
      do
      {
        currentValue = Instance(type, request);
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public object Instance(Type type, GenerationRequest request, params GenerationCustomization[] customizations)
    {
      return _generationChain.Resolve(new CustomizedGenerator(this), request, type);
    }

    public T Instance<T>(GenerationRequest request, params GenerationCustomization[] customizations)
    {
      return (T)Instance(typeof(T), request, customizations);
    }

    public T Dummy<T>()
    {
      var request = CreateRequest();
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

    private static DefaultGenerationRequest CreateRequest(params GenerationCustomization[] customizations)
    {
      return new DefaultGenerationRequest(GlobalNestingLimit.Of(5), customizations);
    }
  }
}
