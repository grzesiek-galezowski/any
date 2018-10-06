using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using NSubstitute.Core;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeReflection;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class AllGenerator : InstanceGenerator, BasicGenerator
  {
    [NonSerialized] private readonly IFakeChainFactory _fakeChainFactory;

    [NonSerialized] private readonly GenericMethodProxyCalls _methodProxyCalls;

    [NonSerialized] private readonly ValueGenerator _valueGenerator;

    public AllGenerator(ValueGenerator valueGenerator,
      IFakeChainFactory fakeChainFactory,
      GenericMethodProxyCalls methodProxyCalls)
    {
      _valueGenerator = valueGenerator;
      _fakeChainFactory = fakeChainFactory;
      _methodProxyCalls = methodProxyCalls;
    }

    public T Instance<T>()
    {
      var trace = new ListBasedGenerationTrace();
      try
      {
        trace.BeginCreatingInstanceGraphWith(typeof(T));
        return Instance<T>(trace);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(trace, e);
      }
    }

    public T Instance<T>(params GenerationCustomization[] customizations)
    {
      var trace = new ListBasedGenerationTrace();
      try
      {
        trace.BeginCreatingInstanceGraphWith(typeof(T));
        return _fakeChainFactory.GetInstance<T>().Resolve(new CustomizedGenerator(this, customizations), trace);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(trace, e);
      }
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      var trace = new ListBasedGenerationTrace();
      try
      {
        trace.BeginCreatingInstanceGraphWithInlineGenerator(typeof(T), gen);
        return gen.GenerateInstance(this, trace);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(trace, e);
      }
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T Value<T>(GenerationTrace trace)
    {
      return _valueGenerator.Value<T>();
    }

    public T Value<T>(T seed, GenerationTrace trace)
    {
      trace.GeneratingSeedeedValue(typeof(T), seed);
      return _valueGenerator.Value(seed);
    }

    public object Instance(Type type, GenerationTrace trace)
    {
      return _methodProxyCalls
        .ResultOfGenericVersionOfMethod(
          this, type, MethodBase.GetCurrentMethod().Name, trace);
    }

    public T Instance<T>(GenerationTrace trace)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(this, trace);
    }

    public T Dummy<T>(GenerationTrace trace)
    {
      var fakeInterface = _fakeChainFactory.CreateFakeOrdinaryInterfaceGenerator<T>();
      var unconstrainedChain = _fakeChainFactory.GetUnconstrainedInstance<T>();

      if (typeof(T).IsPrimitive)
      {
        return unconstrainedChain.Resolve(this, trace);
      }

      if (typeof(T) == typeof(string))
      {
        return unconstrainedChain.Resolve(this, trace);
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
        return default(T);
      }

      if (fakeInterface.Applies())
      {
        return fakeInterface.Apply(this, trace);
      }

      return (T)FormatterServices.GetUninitializedObject(typeof(T));
    }


    public T OtherThan<T>(params T[] omittedValues)
    {
      if (omittedValues == null)
      {
        return Instance<T>();
      }

      T currentValue;
      do
      {
        currentValue = Instance<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public object Instance(Type type, GenerationTrace trace, params GenerationCustomization[] customizations)
    {
      return _methodProxyCalls
        .ResultOfGenericVersionOfMethod(
          new CustomizedGenerator(this, customizations), type, MethodBase.GetCurrentMethod().Name, trace);
    }

    public T Instance<T>(GenerationTrace trace, params GenerationCustomization[] customizations)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(new CustomizedGenerator(this, customizations), trace);
    }

    public T Dummy<T>()
    {
      var trace = new ListBasedGenerationTrace();
      try
      {
        trace.BeginCreatingDummyInstanceOf(typeof(T));
        return Dummy<T>(trace);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(trace, e);
      }
    }
  }

  public class CustomizedGenerator : InstanceGenerator
  {
    private readonly GenerationCustomization[] _customizations;
    private readonly AllGenerator _inner;

    public CustomizedGenerator(AllGenerator inner, GenerationCustomization[] customizations)
    {
      _inner = inner;
      _customizations = customizations;
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _inner.ValueOtherThan(omittedValues);
    }

    public T Value<T>(GenerationTrace trace)
    {
      return _inner.Value<T>(trace);
    }

    public T Value<T>(T seed, GenerationTrace trace)
    {
      return _inner.Value(seed, trace);
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _inner.OtherThan(omittedValues);
    }

    public object Instance(Type type, GenerationTrace trace)
    {
      return _customizations.Where(c => c.AppliesTo(type)).FirstOrNothing()
        .Fold(
          () => _inner.Instance(type, trace, _customizations), 
          c => c.Generate(this, trace));
    }

    public T Dummy<T>(GenerationTrace trace)
    {
      return _inner.Dummy<T>(trace);
    }

    public T Instance<T>(GenerationTrace trace)
    {
      return _inner.Instance<T>(trace, _customizations);
    }
  }
}