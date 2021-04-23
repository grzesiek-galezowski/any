using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using FluentAssertions;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeReflection;
using TddXt.TypeResolution;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class AllGenerator : CustomizableInstanceGenerator, BasicGenerator
  {
    [NonSerialized] private readonly IFakeChainFactory _fakeChainFactory;

    [NonSerialized] private readonly GenericMethodProxyCalls _methodProxyCalls;

    [NonSerialized] private readonly object _syncRoot;

    [NonSerialized] private readonly ValueGenerator _valueGenerator;

    private SynchronizedInstanceGenerator SynchronizedThis => new SynchronizedInstanceGenerator(this, _syncRoot);


    public AllGenerator(ValueGenerator valueGenerator,
      IFakeChainFactory fakeChainFactory,
      GenericMethodProxyCalls methodProxyCalls, 
      object syncRoot)
    {
      _valueGenerator = valueGenerator;
      _fakeChainFactory = fakeChainFactory;
      _methodProxyCalls = methodProxyCalls;
      _syncRoot = syncRoot;
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
      var request = CreateRequest();
      try
      {
        request.Trace.BeginCreatingInstanceGraphWith(typeof(T));
        return _fakeChainFactory.GetInstance<T>().Resolve(CreateCustomizedInstanceGenerator<T>(customizations), request);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(request, e);
      }
    }

    private InstanceGenerator CreateCustomizedInstanceGenerator<T>(GenerationCustomization[] customizations)
    {
      return new CustomizedGenerator(SynchronizedThis, customizations);
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      var request = CreateRequest();
      try
      {
        request.Trace.BeginCreatingInstanceGraphWithInlineGenerator(typeof(T), gen);
        return gen.GenerateInstance(SynchronizedThis, request);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(request, e);
      }
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T Value<T>(GenerationRequest request)
    {
      return _valueGenerator.Value<T>();
    }

    public T Value<T>(GenerationRequest request, GenerationCustomization[] customizations)
    {
      return _valueGenerator.Value<T>(CreateCustomizedInstanceGenerator<T>(customizations), customizations, request);
    }

    public T Value<T>(T seed, GenerationRequest request)
    {
      request.Trace.GeneratingSeededValue(typeof(T), seed);
      return _valueGenerator.Value(seed);
    }

    public object Instance(Type type, GenerationRequest request)
    {
      return _methodProxyCalls
        .ResultOfGenericVersionOfMethod(
          SynchronizedThis, type, MethodBase.GetCurrentMethod().Name, request);
    }

    public T Instance<T>(GenerationRequest request)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(SynchronizedThis, request);
    }

    public T Dummy<T>(GenerationRequest request)
    {
      var fakeInterface = _fakeChainFactory.CreateFakeOrdinaryInterfaceGenerator<T>();
      var unconstrainedChain = _fakeChainFactory.GetUnconstrainedInstance<T>();

      if (typeof(T).IsPrimitive)
      {
        return unconstrainedChain.Resolve(SynchronizedThis, request);
      }

      if (typeof(T) == typeof(string))
      {
        return unconstrainedChain.Resolve(SynchronizedThis, request);
      }

      var emptyCollectionInstantiation = new EmptyCollectionInstantiation();
      if (TypeOf<T>.IsImplementationOfOpenGeneric(typeof(IEnumerable<>)))
      {
        return emptyCollectionInstantiation.CreateCollectionPassedAsGenericType<T>();
      }

      if (TypeOf<T>.IsOpenGeneric(typeof(IEnumerable<>)))
      {
        return (T)emptyCollectionInstantiation.EmptyEnumerableOf(typeof(T).GetCollectionItemType());
      }

      if (typeof(T).IsAbstract)
      {
        return default!;
      }

      if (fakeInterface.Applies())
      {
        return fakeInterface.Apply(SynchronizedThis, request);
      }

      return (T)FormatterServices.GetUninitializedObject(typeof(T));
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
      return _methodProxyCalls
        .ResultOfGenericVersionOfMethod(
          new CustomizedGenerator(SynchronizedThis, customizations), 
          type, 
          MethodBase.GetCurrentMethod().Name, 
          request);
    }

    public T Instance<T>(GenerationRequest request, params GenerationCustomization[] customizations)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(new CustomizedGenerator(SynchronizedThis, customizations), request);
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

    private static DefaultGenerationRequest CreateRequest()
    {
      return new DefaultGenerationRequest(GlobalNestingLimit.Of(5));
    }
  }
}