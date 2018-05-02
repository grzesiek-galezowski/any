using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class AllGenerator : InstanceGenerator
  {
    public AllGenerator(
      ValueGenerator valueGenerator, 
      SpecificTypeObjectGenerator specificTypeObjectGenerator, 
      StringGenerator stringGenerator, 
      EmptyCollectionGenerator emptyCollectionGenerator, 
      NumericGenerator numericGenerator, 
      ProxyBasedGenerator genericGenerator, 
      InvokableGenerator invokableGenerator)
    {
      _valueGenerator = valueGenerator;
      _specificTypeObjectGenerator = specificTypeObjectGenerator;
      _stringGenerator = stringGenerator;
      _emptyCollectionGenerator = emptyCollectionGenerator;
      _numericGenerator = numericGenerator;
      _genericGenerator = genericGenerator;
      _invokableGenerator = invokableGenerator;
    }

    public const int Many = 3;

    private readonly Random _randomGenerator = new Random(System.Guid.NewGuid().GetHashCode());
    private readonly StringGenerator _stringGenerator;
    private readonly ValueGenerator _valueGenerator;
    private readonly EmptyCollectionGenerator _emptyCollectionGenerator;
    private readonly SpecificTypeObjectGenerator _specificTypeObjectGenerator;
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

    public Uri Uri()
    {
      return _specificTypeObjectGenerator.Uri();
    }

    public Guid Guid()
    {
      return _specificTypeObjectGenerator.Guid();
    }

    public int Port()
    {
      return _randomGenerator.Next(65535);
    }

    public string Ip()
    {
      return _stringGenerator.Ip(this);
    }

    public object EmptyEnumerableOf(Type collectionType)
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf(collectionType);
    }

    public T InstanceOtherThanObjects<T>(params object[] omittedValues)
    {
      return _genericGenerator.InstanceOtherThanObjects<T>(omittedValues);
    }

    public IEnumerable<T> EnumerableWith<T>(IEnumerable<T> included)
    {
      return InlineGenerators.EnumerableWith(included).GenerateInstance(this);
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

    public object List(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.List), type);
    }

    private object GenerateByNameAndType(string methodName, Type type)
    {
      return InlineGenerators.GetByNameAndType(methodName, type).GenerateInstance(this);
    }

    private object GenerateByNameAndTypes(string methodName, Type type1, Type type2)
    {
      return InlineGenerators.GetByNameAndTypes(methodName, type1, type2)
        .GenerateInstance(this);
    }

    public object Set(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.Set), type);
    }

    public object SortedList(Type keyType, Type valueType)
    {
      return GenerateByNameAndTypes(nameof(InlineGenerators.SortedList), keyType, valueType);
    }

    public object SortedSet(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.SortedSet), type);
    }

    public object ConcurrentDictionary(Type keyType, Type valueType)
    {
      return GenerateByNameAndTypes(nameof(InlineGenerators.ConcurrentDictionary), keyType, valueType);
    }

    public object Array(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.Array), type);
    }

    public object Enumerator(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.Enumerator), type);
    }

    public object ConcurrentStack(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.ConcurrentStack), type);
    }

    public object ConcurrentQueue(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.ConcurrentQueue), type);
    }

    public object ConcurrentBag(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.ConcurrentBag), type);
    }

    public string StringOfLength(int charactersCount)
    {
      return InlineGenerators.String(charactersCount).GenerateInstance(this);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return _stringGenerator.StringOtherThan(alreadyUsedStrings);
    }

    public string StringNotContaining<T>(params T[] excludedObjects)
    {
      return _stringGenerator.StringNotContaining(this, excludedObjects);
    }

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      return _stringGenerator.StringNotContaining(this, excludedSubstrings);
    }

    public string StringContaining<T>(T obj)
    {
      return _stringGenerator.StringContaining(obj, this);
    }

    public string StringContaining(string str)
    {
      return _stringGenerator.StringContaining(str, this);
    }

    public string AlphaString()
    {
      return InlineGenerators.AlphaString(
        InlineGenerators.String().GenerateInstance(this).Length)
        .GenerateInstance(this);
    }

    public string AlphaString(int maxLength)
    {
      return InlineGenerators.AlphaString(maxLength).GenerateInstance(this);
    }

    public string Identifier()
    {
      return InlineGenerators.Identifier().GenerateInstance(this);
    }

    public byte Digit()
    {
      return _numericGenerator.Digit();
    }

    public int IntegerFromSequence(int startingValue = 0, int step = 1)
    {
      return _numericGenerator.IntegerFromSequence(startingValue, step);
    }

    public byte Octet()
    {
      return _numericGenerator.Octet();
    }

    public int IntegerDivisibleBy(int quotient)
    {
      return _numericGenerator.IntegerDivisibleBy(quotient);
    }

    public int IntegerNotDivisibleBy(int quotient)
    {
      return _numericGenerator.IntegerNotDivisibleBy(quotient);
    }

    public int IntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.IntegerWithExactDigitsCount(digitsCount);
    }

    public long LongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.LongIntegerWithExactDigitsCount(digitsCount);
    }

    public uint UnsignedIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.UnsignedIntegerWithExactDigitsCount(digitsCount);
    }

    public ulong UnsignedLongIntegerWithExactDigitsCount(int digitsCount)
    {
      return _numericGenerator.UnsignedLongIntegerWithExactDigitsCount(digitsCount);
    }

    public byte PositiveDigit()
    {
      return _numericGenerator.PositiveDigit();
    }

    public T Exploding<T>() where T : class
    {
      return _genericGenerator.Exploding<T>();
    }

    public int Integer()
    {
      return _numericGenerator.Integer();
    }

    public double Double()
    {
      return _numericGenerator.Double();
    }

    public double DoubleOtherThan(double[] others)
    {
      return _numericGenerator.DoubleOtherThan(others);
    }

    public long LongInteger()
    {
      return _numericGenerator.LongInteger();
    }

    public long LongIntegerOtherThan(long[] others)
    {
      return _numericGenerator.LongIntegerOtherThan(others);
    }

    public ulong UnsignedLongInteger()
    {
      return _numericGenerator.UnsignedLongInteger();
    }

    public ulong UnsignedLongIntegerOtherThan(ulong[] others)
    {
      return _numericGenerator.UnsignedLongIntegerOtherThan(others);
    }

    public int IntegerOtherThan(int[] others)
    {
      return _numericGenerator.IntegerOtherThan(others);
    }

    public byte Byte()
    {
      return _numericGenerator.Byte();
    }

    public byte ByteOtherThan(byte[] others)
    {
      return _numericGenerator.ByteOtherThan(others);
    }

    public decimal Decimal()
    {
      return _numericGenerator.Decimal();
    }

    public decimal DecimalOtherThan(decimal[] others)
    {
      return _numericGenerator.DecimalOtherThan(others);
    }

    public uint UnsignedInt()
    {
      return _numericGenerator.UnsignedInt();
    }

    public uint UnsignedIntOtherThan(uint[] others)
    {
      return _numericGenerator.UnsignedIntOtherThan(others);
    }

    public ushort UnsignedShort()
    {
      return _numericGenerator.UnsignedShort();
    }

    public ushort UnsignedShortOtherThan(ushort[] others)
    {
      return _numericGenerator.UnsignedShortOtherThan(others);
    }

    public short ShortInteger()
    {
      return _numericGenerator.ShortInteger();
    }

    public short ShortIntegerOtherThan(short[] others)
    {
      return _numericGenerator.ShortIntegerOtherThan(others);
    }

    public MethodInfo FindEmptyGenericsMethod<T>(string name)
    {
      var methods = typeof(T).GetMethods(
          BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
        .Where(m => m.IsGenericMethodDefinition)
        .Where(m => !m.GetParameters().Any());
      var method = methods.First(m => m.Name == name);
      return method;
    }
  }
}