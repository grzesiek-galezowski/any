using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.CommonTypes;
using TddXt.TypeReflection;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class AllGenerator : InstanceGenerator
  {
    public AllGenerator(ValueGenerator valueGenerator,
      IFakeChainFactory fakeChainFactory,
      GenericMethodProxyCalls methodProxyCalls)
    {
      _valueGenerator = valueGenerator;
      _fakeChainFactory = fakeChainFactory;
      _methodProxyCalls = methodProxyCalls;
    }

    [NonSerialized] private readonly ValueGenerator _valueGenerator;
    [NonSerialized] private readonly IFakeChainFactory _fakeChainFactory;

    [NonSerialized] private readonly GenericMethodProxyCalls _methodProxyCalls;

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T Value<T>()
    {
      return _valueGenerator.Value<T>();
    }

    public T Value<T>(T seed)
    {
      return _valueGenerator.Value(seed);
    }

    public object Instance(Type type)
    {
      return ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }

    private object ResultOfGenericVersionOfMethod<T>(T instance, Type type, string name)
    {
      //todo do something with this...
      return _methodProxyCalls.ResultOfGenericVersionOfMethod(instance, type, name);
    }

    public T Instance<T>()
    {
      var trace = new GenerationTrace();
      return _fakeChainFactory.GetInstance<T>().Resolve(this, trace);
    }

    public T Dummy<T>()
    {
      var trace = new GenerationTrace();
      return Dummy<T>(trace);
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
        return fakeInterface.Apply(this);
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

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      return gen.GenerateInstance(this);
    }
  }
}