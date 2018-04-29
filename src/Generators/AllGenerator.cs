using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Generators;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class AllGenerator : InstanceGenerator
  {
    public AllGenerator(
      ValueGenerator valueGenerator, 
      CharGenerator charGenerator, 
      SpecificTypeObjectGenerator specificTypeObjectGenerator, 
      StringGenerator stringGenerator, 
      EmptyCollectionGenerator emptyCollectionGenerator, 
      NumericGenerator numericGenerator, 
      ProxyBasedGenerator genericGenerator, 
      InvokableGenerator invokableGenerator)
    {
      _valueGenerator = valueGenerator;
      _charGenerator = charGenerator;
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
    private readonly CharGenerator _charGenerator;
    private readonly SpecificTypeObjectGenerator _specificTypeObjectGenerator;
    private readonly ProxyBasedGenerator _genericGenerator;
    private readonly NumericGenerator _numericGenerator;
    private readonly InvokableGenerator _invokableGenerator;

    public IPAddress IpAddress()
    {
      return _valueGenerator.ValueOf<IPAddress>();
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _valueGenerator.ValueOtherThan(omittedValues);
    }

    public T From<T>(params T[] possibleValues)
    {
      return InlineGenerators.From(possibleValues)
        .GenerateInstance(_genericGenerator);
    }

    public DateTime DateTime()
    {
      return ValueOf<DateTime>();
    }

    public TimeSpan TimeSpan()
    {
      return ValueOf<TimeSpan>();
    }

    public T ValueOf<T>()
    {
      return _valueGenerator.ValueOf<T>();
    }

    public IEnumerable<T> EmptyEnumerableOf<T>()
    {
      return _emptyCollectionGenerator.EmptyEnumerableOf<T>();
    }

    public object Instance(Type type)
    {
      return _genericGenerator.Instance(type);
    }

    public string LegalXmlTagName()
    {
      return _stringGenerator.LegalXmlTagName();
    }

    public bool Boolean()
    {
      return ValueOf<bool>();
    }

    public object Object()
    {
      return ValueOf<object>();
    }

    public MethodInfo Method()
    {
      return ValueOf<MethodInfo>();
    }

    public Type Type()
    {
      return ValueOf<Type>();
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

    public string UrlString()
    {
      return _stringGenerator.UrlString();
    }

    public Exception Exception()
    {
      return ValueOf<Exception>();
    }

    public int Port()
    {
      return _randomGenerator.Next(65535);
    }

    public string Ip()
    {
      return _stringGenerator.Ip();
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
      return InlineGenerators.EnumerableWith(included).GenerateInstance(_genericGenerator);
    }

    public Task NotStartedTask()
    {
      return _invokableGenerator.NotStartedTask();
    }

    public Task<T> NotStartedTask<T>()
    {
      return _invokableGenerator.NotStartedTask<T>(_genericGenerator);
    }

    public Task<T> StartedTask<T>()
    {
      return _invokableGenerator.StartedTask<T>(_genericGenerator);
    }

    public Func<T> Func<T>()
    {
      return _invokableGenerator.Func<T>(_genericGenerator);
    }

    public Func<T1, T2> Func<T1, T2>()
    {
      return _invokableGenerator.Func<T1, T2>(_genericGenerator);
    }

    public Func<T1, T2, T3> Func<T1, T2, T3>()
    {
      return _invokableGenerator.Func<T1, T2, T3>(_genericGenerator);
    }

    public Func<T1, T2, T3, T4> Func<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4>(_genericGenerator);
    }

    public Func<T1, T2, T3, T4, T5> Func<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5>(_genericGenerator);
    }

    public Func<T1, T2, T3, T4, T5, T6> Func<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Func<T1, T2, T3, T4, T5, T6>(_genericGenerator);
    }

    public Action Action()
    {
      return _invokableGenerator.Action(_genericGenerator);
    }

    public Action<T> Action<T>()
    {
      return _invokableGenerator.Action<T>(_genericGenerator);
    }

    public Action<T1, T2> Action<T1, T2>()
    {
      return _invokableGenerator.Action<T1, T2>(_genericGenerator);
    }

    public Action<T1, T2, T3> Action<T1, T2, T3>()
    {
      return _invokableGenerator.Action<T1, T2, T3>(_genericGenerator);
    }

    public Action<T1, T2, T3, T4> Action<T1, T2, T3, T4>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4>(_genericGenerator);
    }

    public Action<T1, T2, T3, T4, T5> Action<T1, T2, T3, T4, T5>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5>(_genericGenerator);
    }

    public Action<T1, T2, T3, T4, T5, T6> Action<T1, T2, T3, T4, T5, T6>()
    {
      return _invokableGenerator.Action<T1, T2, T3, T4, T5, T6>(_genericGenerator);
    }

    public T Of<T>() where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return ValueOf<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">MUST BE AN ENUM. FOR NORMAL VALUES, USE AllGenerator.OtherThan()</typeparam>
    /// <param name="excludedValues"></param>
    /// <returns></returns>
    public T Besides<[MustBeAnEnum] T>([MustBeAnEnum] params T[] excludedValues) where T : struct, IConvertible
    {
      AssertDynamicEnumConstraintFor<T>();
      return ValueOtherThan(excludedValues);
    }

    public IReadOnlyList<T> ReadOnlyList<T>()
    {
      return InlineGenerators.List<T>().GenerateInstance(_genericGenerator);
    }

    public IReadOnlyList<T> ReadOnlyList<T>(int length)
    {
      return InlineGenerators.List<T>(length).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(IEnumerable<T> items)
    {
      return InlineGenerators.ListWith(items.ToArray()).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWith<T>(params T[] items)
    {
      return InlineGenerators.ListWith(items).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(IEnumerable<T> items)
    {
      return InlineGenerators.ListWithout(items.ToArray()).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyList<T> ReadOnlyListWithout<T>(params T[] items)
    {
      return InlineGenerators.ListWithout(items).GenerateInstance(_genericGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>()
    {
      return InlineGenerators.SortedList<TKey, TValue>().GenerateInstance(_genericGenerator);
    }

    public SortedList<TKey, TValue> SortedList<TKey, TValue>(int length)
    {
      return InlineGenerators.SortedList<TKey, TValue>(length).GenerateInstance(_genericGenerator);
    }

    public ISet<T> Set<T>(int length)
    {
      return InlineGenerators.Set<T>(length).GenerateInstance(_genericGenerator);
    }

    public ISet<T> Set<T>()
    {
      return InlineGenerators.Set<T>(Many).GenerateInstance(_genericGenerator);
    }

    public ISet<T> SortedSet<T>(int length)
    {
      return InlineGenerators.SortedSet<T>(length).GenerateInstance(_genericGenerator);
    }

    public ISet<T> SortedSet<T>()
    {
      return InlineGenerators.SortedSet<T>().GenerateInstance(_genericGenerator);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>(int length)
    {
      return InlineGenerators.Dictionary<TKey, TValue>(length).GenerateInstance(_genericGenerator);
    }

    public Dictionary<TKey, TValue> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return new DictionaryWithKeysGenerator<TKey, TValue>(keys).GenerateInstance(_genericGenerator);
    }

    public Dictionary<TKey, TValue> Dictionary<TKey, TValue>()
    {
      return InlineGenerators.Dictionary<TKey, TValue>().GenerateInstance(_genericGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return InlineGenerators.Dictionary<TKey, TValue>(length).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return InlineGenerators.DictionaryWithKeys2<TKey, TValue>(keys).GenerateInstance(_genericGenerator);
    }

    public IReadOnlyDictionary<TKey, TValue> ReadOnlyDictionary<TKey, TValue>()
    {
      return InlineGenerators.Dictionary<TKey, TValue>().GenerateInstance(_genericGenerator);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>(int length)
    {
      return InlineGenerators.SortedDictionary<TKey, TValue>(length).GenerateInstance(_genericGenerator);
    }

    public SortedDictionary<TKey, TValue> SortedDictionary<TKey, TValue>()
    {
      return InlineGenerators.SortedDictionary<TKey, TValue>()
        .GenerateInstance(_genericGenerator);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return InlineGenerators.ConcurrentDictionary<TKey, TValue>(length).GenerateInstance(_genericGenerator);
    }

    public ConcurrentDictionary<TKey, TValue> ConcurrentDictionary<TKey, TValue>()
    {
      return InlineGenerators.ConcurrentDictionary<TKey, TValue>().GenerateInstance(_genericGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>()
    {
      return InlineGenerators.ConcurrentStack<T>().GenerateInstance(_genericGenerator);
    }

    public ConcurrentStack<T> ConcurrentStack<T>(int length)
    {
      return InlineGenerators.ConcurrentStack<T>(length).GenerateInstance(_genericGenerator);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>()
    {
      return InlineGenerators.ConcurrentQueue<T>().GenerateInstance(_genericGenerator);
    }

    public ConcurrentQueue<T> ConcurrentQueue<T>(int length)
    {
      return InlineGenerators.ConcurrentQueue<T>(length).GenerateInstance(_genericGenerator);
    }

    public ConcurrentBag<T> ConcurrentBag<T>()
    {
      return InlineGenerators.ConcurrentBag<T>().GenerateInstance(_genericGenerator);
    }

    public ConcurrentBag<T> ConcurrentBag<T>(int length)
    {
      return InlineGenerators.ConcurrentBag<T>(length).GenerateInstance(_genericGenerator);
    }

    public IEnumerable<T> EnumerableSortedDescending<T>(int length)
    {
      return InlineGenerators.SortedSet<T>(length).GenerateInstance(_genericGenerator);
    }

    public IEnumerable<T> EnumerableSortedDescending<T>()
    {
      return InlineGenerators.SortedSet<T>(AllGenerator.Many).GenerateInstance(_genericGenerator);
    }

    public IEnumerator<T> Enumerator<T>()
    {
      return InlineGenerators.Enumerator<T>().GenerateInstance(_genericGenerator);
    }

    public object List(Type type)
    {
      return GenerateByNameAndType(nameof(InlineGenerators.List), type);
    }

    private object GenerateByNameAndType(string methodName, Type type)
    {
      return InlineGenerators.GetByNameAndType(methodName, type).GenerateInstance(_genericGenerator);
    }

    private object GenerateByNameAndTypes(string methodName, Type type1, Type type2)
    {
      return InlineGenerators.GetByNameAndTypes(methodName, type1, type2)
        .GenerateInstance(_genericGenerator);
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

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many)
    {
      return CollectionFiller.FillingCollection(collection, many, _genericGenerator);
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

    public string String()
    {
      return _stringGenerator.String();
    }

    public string String(string seed)
    {
      return _stringGenerator.String(seed);
    }

    public string LowerCaseString()
    {
      return _stringGenerator.LowerCaseString();
    }

    public string UpperCaseString()
    {
      return _stringGenerator.UpperCaseString();
    }

    public string LowerCaseAlphaString()
    {
      return _stringGenerator.LowerCaseAlphaString();
    }

    public string UpperCaseAlphaString()
    {
      return _stringGenerator.UpperCaseAlphaString();
    }

    public string StringMatching(string pattern)
    {
      return _stringGenerator.StringMatching(pattern);
    }

    public string StringOfLength(int charactersCount)
    {
      return _stringGenerator.StringOfLength(charactersCount);
    }

    public string StringOtherThan(params string[] alreadyUsedStrings)
    {
      return _stringGenerator.StringOtherThan(alreadyUsedStrings);
    }

    public string StringNotContaining<T>(params T[] excludedObjects)
    {
      return _stringGenerator.StringNotContaining(excludedObjects);
    }

    public string StringNotContaining(params string[] excludedSubstrings)
    {
      return _stringGenerator.StringNotContaining(excludedSubstrings);
    }

    public string StringContaining<T>(T obj)
    {
      return _stringGenerator.StringContaining(obj);
    }

    public string StringContaining(string str)
    {
      return _stringGenerator.StringContaining(str);
    }

    public string AlphaString()
    {
      return _stringGenerator.AlphaString();
    }

    public string AlphaString(int maxLength)
    {
      return _stringGenerator.AlphaString(maxLength);
    }

    public string Identifier()
    {
      return _stringGenerator.Identifier();
    }

    public string NumericString(int digitsCount = Many)
    {
      return _stringGenerator.NumericString(digitsCount);
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

    private static void AssertDynamicEnumConstraintFor<T>()
    {
      if (!typeof(T).IsEnum)
      {
        throw new ArgumentException("T must be an enum type. For other types, use AllGenerator.OtherThan()");
      }
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


    public char AlphaChar()
    {
      return _charGenerator.AlphaChar();
    }

    public char DigitChar()
    {
      return _charGenerator.DigitChar();
    }

    public char Char()
    {
      return _charGenerator.Char();
    }

    public char LowerCaseAlphaChar()
    {
      return _charGenerator.LowerCaseAlphaChar();
    }

    public char UpperCaseAlphaChar()
    {
      return _charGenerator.UpperCaseAlphaChar();
    }
  }
}