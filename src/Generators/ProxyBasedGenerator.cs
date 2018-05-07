using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Castle.DynamicProxy;
using NSubstitute;
using AutoFixture;
using Generators.Inline;
using TddEbook.TddToolkit.TypeResolution.FakeChainElements;
using TddEbook.TddToolkit.TypeResolution.Interceptors;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{

  [Serializable]
  public class ProxyBasedGenerator : InstanceGenerator
  {
    [NonSerialized]
    private readonly ProxyGenerator _proxyGenerator;
    [NonSerialized]
    private readonly FakeChainFactory _fakeChainFactory;
    [NonSerialized]
    private readonly ValueGenerator _valueGenerator;

    [NonSerialized]
    private readonly Fixture _emptyCollectionFixture;
    [NonSerialized]
    private readonly GenericMethodProxyCalls _proxyCalls;
    [NonSerialized]
    private readonly EmptyCollectionInstantiation _emptyCollectionInstantiation;

    public ProxyBasedGenerator(Fixture emptyCollectionFixture,
      GenericMethodProxyCalls proxyCalls,
      EmptyCollectionInstantiation emptyCollectionInstantiation,
      ProxyGenerator proxyGenerator,
      FakeChainFactory fakeChainFactory, 
      ValueGenerator valueGenerator)
    {
      _emptyCollectionFixture = emptyCollectionFixture;
      _proxyGenerator = proxyGenerator;
      _fakeChainFactory = fakeChainFactory;
      _valueGenerator = valueGenerator;
      _proxyCalls = proxyCalls;
      _emptyCollectionInstantiation = emptyCollectionInstantiation;
    }

    public T Instance<T>()
    {
      return _fakeChainFactory.GetInstance<T>().Resolve(this);
    }

    public T Dummy<T>()
    {
      var fakeInterface = _fakeChainFactory.CreateFakeOrdinaryInterfaceGenerator<T>();

      if (typeof(T).IsPrimitive)
      {
        return _fakeChainFactory.GetUnconstrainedInstance<T>().Resolve(this);
      }
      if (typeof(T) == typeof(string))
      {
        return _fakeChainFactory.GetUnconstrainedInstance<T>().Resolve(this);
      }
      if (TypeOf<T>.IsImplementationOfOpenGeneric(typeof (IEnumerable<>)))
      {
        return _emptyCollectionFixture.Create<T>();
      }
      if (TypeOf<T>.IsOpenGeneric(typeof(IEnumerable<>)))
      {
        return (T) _emptyCollectionInstantiation.EmptyEnumerableOf(typeof(T).GetCollectionItemType());
      }
      if (typeof(T).IsAbstract)
      {
        return default(T);
      }
      if (fakeInterface.Applies())
      {
        return fakeInterface.Apply(this);
      }
      return (T)FormatterServices.GetUninitializedObject(typeof (T));
    }

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

    public T Substitute<T>(InstanceGenerator allGenerator) where T : class
    {
      return InlineGenerators.Substitute<T>().GenerateInstance(allGenerator);
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

    public T Exploding<T>() where T : class
    {
      if (typeof(T).IsInterface)
      {
        return _proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(new ExplodingInterceptor());
      }
      else
      {
        throw new Exception("Exploding instances can be created out of interfaces only!");
      }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return OtherThan(omittedValues.Cast<T>().ToArray());
    }

    public object Instance(Type type)
    {
      return ResultOfGenericVersionOfMethod(this, type, MethodBase.GetCurrentMethod().Name);
    }

    public object ResultOfGenericVersionOfMethod<T>(T instance, Type keyType, Type valueType, string name)
    {
      return _proxyCalls.ResultOfGenericVersionOfMethod(instance, keyType, valueType, name);
    }

    public object ResultOfGenericVersionOfMethod<T>(T instance, Type type, string name)
    {
      return _proxyCalls.ResultOfGenericVersionOfMethod(instance, type, name);
    }
  }
}

public static class TypeExtensions
{
  public static Type GetCollectionItemType(this Type type)
  {
    if (type.GetTypeInfo().IsArray)
      return type.GetElementType();
    if (type.GetTypeInfo().IsGenericType)
      return type.GetGenericArguments().Single();
    throw new ArgumentException(
      "The argument is not a valid collection type.", "type");
  }
}