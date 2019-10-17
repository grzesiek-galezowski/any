using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeReflection;

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
        return _fakeChainFactory.GetInstance<T>().Resolve(CreateCustomizedInstanceGenerator<T>(customizations), trace);
      }
      catch (Exception e)
      {
        throw new GenerationFailedException(trace, e);
      }
    }

    private InstanceGenerator CreateCustomizedInstanceGenerator<T>(GenerationCustomization[] customizations)
    {
      return new CustomizedGenerator(SynchronizedThis, customizations);
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      var trace = new ListBasedGenerationTrace();
      try
      {
        trace.BeginCreatingInstanceGraphWithInlineGenerator(typeof(T), gen);
        return gen.GenerateInstance(SynchronizedThis, trace);
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

    public T Value<T>(GenerationTrace trace, GenerationCustomization[] customizations)
    {
      return _valueGenerator.Value<T>(CreateCustomizedInstanceGenerator<T>(customizations), customizations, trace);
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
          SynchronizedThis, type, MethodBase.GetCurrentMethod().Name, trace);
    }

    public T Instance<T>(GenerationTrace trace)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(SynchronizedThis, trace);
    }

    public T Dummy<T>(GenerationTrace trace)
    {
      var fakeInterface = _fakeChainFactory.CreateFakeOrdinaryInterfaceGenerator<T>();
      var unconstrainedChain = _fakeChainFactory.GetUnconstrainedInstance<T>();

      if (typeof(T).IsPrimitive)
      {
        return unconstrainedChain.Resolve(SynchronizedThis, trace);
      }

      if (typeof(T) == typeof(string))
      {
        return unconstrainedChain.Resolve(SynchronizedThis, trace);
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
        return fakeInterface.Apply(SynchronizedThis, trace);
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
          new CustomizedGenerator(SynchronizedThis, customizations), type, MethodBase.GetCurrentMethod().Name, trace);
    }

    public T Instance<T>(GenerationTrace trace, params GenerationCustomization[] customizations)
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(new CustomizedGenerator(SynchronizedThis, customizations), trace);
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
}