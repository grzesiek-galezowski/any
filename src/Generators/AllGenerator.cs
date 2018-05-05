using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generators;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class AllGenerator : InstanceGenerator
  {
    public AllGenerator(
      ValueGenerator valueGenerator, 
      EmptyCollectionGenerator emptyCollectionGenerator, 
      NumericGenerator numericGenerator, 
      ProxyBasedGenerator genericGenerator, 
      InvokableGenerator invokableGenerator)
    {
      _valueGenerator = valueGenerator;
      _emptyCollectionGenerator = emptyCollectionGenerator;
      _numericGenerator = numericGenerator;
      _genericGenerator = genericGenerator;
      _invokableGenerator = invokableGenerator;
    }

    public const int Many = 3;

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly ValueGenerator _valueGenerator;
    private readonly EmptyCollectionGenerator _emptyCollectionGenerator;
    private readonly ProxyBasedGenerator _genericGenerator;
    private readonly NumericGenerator _numericGenerator;
    private readonly InvokableGenerator _invokableGenerator;

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T ValueOf<T>()
    {
      return _valueGenerator.ValueOf<T>();
    }

    public T ValueOf<T>(T seed)
    {
      return _valueGenerator.ValueOf(seed);
    }

    public IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf<T>();
    }

    public object Instance(Type type)
    {
      return _genericGenerator.Instance(type);
    }

    public T InstanceOf<T>()
    {
      return _genericGenerator.InstanceOf<T>();
    }

    public T Instance<T>()
    {
      return _genericGenerator.Instance<T>();
    }

    public T Dummy<T>()
    {
      return _genericGenerator.Dummy<T>();
    }

    public T SubstituteOf<T>() where T : class
    {
      return _genericGenerator.SubstituteOf<T>();
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _genericGenerator.OtherThan(omittedValues);
    }

    public int Port()
    {
      return _randomGenerator.Next(65535);
    }

    public string Ip()
    {
      return new IpStringGenerator().GenerateInstance(this);
    }

    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return _genericGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }

    public Task NotStartedTask()
    {
      return _invokableGenerator.NotStartedTask();
    }

    public Task<T> NotStartedTask<T>()
    {
      return _invokableGenerator.NotStartedTask<T>(this);
    }

    public Task<T> StartedTask<T>()
    {
      return _invokableGenerator.StartedTask<T>(this);
    }

    public Func<T> Func<T>()
    {
      return _invokableGenerator.Func<T>(this);
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return _invokableGenerator.Func<T1, T2>(this);
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _invokableGenerator.Func<T1, T2, T3>(this);
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4>(this);
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5>(this);
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5, T6>(this);
    }

    public Action Action()
    {
      return _invokableGenerator.Action(this);
    }

    public Action<T> Action<T>()
    {
      return _invokableGenerator.Action<T>(this);
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return _invokableGenerator.Action<T1, T2>(this);
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _invokableGenerator.Action<T1, T2, T3>(this);
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4>(this);
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5>(this);
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5, T6>(this);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return InlineGenerators.DictionaryWithKeys<TKey, TValue>(keys).GenerateInstance(this);
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return InlineGenerators.SortedSet<T>(length).GenerateInstance(this);
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return _numericGenerator.IntegerNotDivisibleBy(quotient, this);
    }

    public T Exploding<T>() where T : class
    {
      return _genericGenerator.Exploding<T>();
    }
  }
}