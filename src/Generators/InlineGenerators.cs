using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Generators;
using Generators.Inline;

namespace TddEbook.TddToolkit.Generators
{
  public class InlineGenerators
  {
    public static InlineGenerator<IEnumerable<T>> EnumerableWith<T>(IEnumerable<T> included)
    {
      return new InclusiveEnumerableGenerator<T>(included);
    }

    public static InlineGenerator<IEnumerable<T>> Enumerable<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many);
    }

    public static InlineGenerator<IEnumerable<T>> Enumerable<T>(int length)
    {
      return new EnumerableGenerator<T>(length);
    }

    public static InlineGenerator<IEnumerable<T>> EnumerableWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded);
    }

    public static InlineGenerator<T[]> Array<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsArray();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<T[]> Array<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsArray();
    }

    public static InlineGenerator<T[]> ArrayWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded).AsArray();
    }

    public static InlineGenerator<T[]> ArrayWith<T>(T[] included)
    {
      return new InclusiveEnumerableGenerator<T>(included).AsArray();
    }

    public static InlineGenerator<List<T>> List<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsList();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<List<T>> List<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsList();
    }

    public static InlineGenerator<List<T>> ListWithout<T>(T[] excluded)
    {
      return new ExclusiveEnumerableGenerator<T>(excluded).AsList();
    }

    public static InlineGenerator<List<T>> ListWith<T>(T[] included)
    {
      return new InclusiveEnumerableGenerator<T>(included).AsList();
    }

    public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsSortedList();
    }

    public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsSortedList();
    }

    public static InlineGenerator<ISet<T>> Set<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<ISet<T>> Set<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSet();
    }

    public static InlineGenerator<SortedSet<T>> SortedSet<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSortedSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<SortedSet<T>> SortedSet<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSortedSet();
    }

    public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsReadOnlyDictionary();
    }

    public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsReadOnlyDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsDictionary();
    }


    public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsConcurrentDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsConcurrentDictionary();
    }

    public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentStack();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentStack();
    }

    public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentQueue();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentQueue();
    }

    public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey,TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsSortedDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey,TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsSortedDictionary();
    }

    public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentBag();
    }

    public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentBag();
    }

    public static InlineGenerator<IEnumerator<T>> Enumerator<T>()
    {
      return new EnumeratorGenerator<T>();
    }

    public static InlineGenerator<object> GetByNameAndType(string methodName, Type type)
    {
      return ObjectAdapter.For(methodName, type);
    }

    public static InlineGenerator<object> GetByNameAndTypes(string methodName, Type type1, Type type2)
    {
      return ObjectAdapter.For(methodName, type1, type2);
    }

    public static InlineGenerator<T> From<T>(T[] possibleValues)
    {
      return new GeneratorByPickingFromSpecifiedSetOfValues<T>(possibleValues);
    }

    public static InlineGenerator<Dictionary<TKey, TValue>> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
    {
      return new DictionaryWithKeysGenerator<TKey, TValue>(keys);
    }

    public static InlineGenerator<KeyValuePair<TKey, TValue>> KeyValuePair<TKey, TValue>()
    {
      return new KeyValuePairGenerator<TKey, TValue>();
    }

    //todo some of these can be optimized:

    public static InlineGenerator<char> AlphaChar()
    {
      return new AlphaCharGenerator();
    }

    public static InlineGenerator<char> DigitChar()
    {
      return new DigitCharGenerator();
    }

    public static InlineGenerator<char> LowerCaseAlphaChar()
    {
      return AlphaChar().AsLowerCase();
    }

    public static InlineGenerator<char> UpperCaseAlphaChar()
    {
      return AlphaChar().AsUpperCase();
    }

    public static InlineGenerator<char> Char()
    {
      return new SimpleValueGenerator<char>();
    }

    public static InlineGenerator<string> NumericString(int digitsCount)
    {
      return new StringMatchingRegexGenerator("[1-9][0-9]{" + (digitsCount - 1) + "}");
    }

    public static InlineGenerator<string> String()
    {
      return new SimpleValueGenerator<string>();
    }

    public static InlineGenerator<string> SeededString(string seed)
    {
      return new SeededValueGenerator<string>(seed);
    }

    public static InlineGenerator<string> LowercaseString()
    {
      return new ValueConversion<string, string>(InlineGenerators.String(), s => s.ToLowerInvariant());
    }

    public static InlineGenerator<string> UppercaseString()
    {
      return new ValueConversion<string, string>(InlineGenerators.String(), s => s.ToUpperInvariant());
    }

    public static InlineGenerator<string> StringMatching(string pattern)
    {
      return new StringMatchingRegexGenerator(pattern);
    }

    public static InlineGenerator<string> AlphaString(int maxLength)
    {
      return new StringFromCharsGenerator(maxLength, InlineGenerators.AlphaChar());
    }

    public static InlineGenerator<string> LowercaseAlphaString()
    {
      return new ValueConversion<string, string>(
        AlphaString(System.Guid.NewGuid().ToString().Length), s => s.ToLowerInvariant());
    }

    public static InlineGenerator<string> UppercaseAlphaString()
    {
      return new ValueConversion<string, string>(
        AlphaString(System.Guid.NewGuid().ToString().Length), s => s.ToUpperInvariant());
    }

    public static InlineGenerator<string> String(int length)
    {
      return new StringOfLengthGenerator(length, InlineGenerators.String());
    }

    public static InlineGenerator<string> Identifier()
    {
      return new IdentifierStringGenerator(InlineGenerators.DigitChar(), InlineGenerators.AlphaChar());
    }

    public static InlineGenerator<string> UrlString()
    {
      return new ValueConversion<Uri, string>(new SimpleValueGenerator<Uri>(), u => u.ToString());
    }

    public static InlineGenerator<string> StringNotContaining(string[] excludedSubstrings)
    {
      return new StringNotContainingGenerator(excludedSubstrings, InlineGenerators.String());
    }

    public static InlineGenerator<string> StringNotContaining<T>(T[] excludedObjects)
    {
      return InlineGenerators.StringNotContaining((from obj in new[] {new[] {excludedObjects}} select obj.ToString()).ToArray());
    }

    public static InlineGenerator<T> ValueOtherThan<T>(T[] excluded)
    {
      return new SimpleValueOtherThanGenerator<T>(excluded);
    }

    public static InlineGenerator<string> StringContaining(string str)
    {
      return new AggregatingGenerator<string>(
        System.String.Empty, 
        (current, next) => current + next, 
        new SimpleValueGenerator<string>(), 
        new FixedValueGenerator<string>(str), 
        new SimpleValueGenerator<string>());
    }

    public static InlineGenerator<Guid> Guid()
    {
      return new SimpleValueGenerator<Guid>();
    }

    public static InlineGenerator<Uri> Uri()
    {
      return new SimpleValueGenerator<Uri>();
    }

    public static InlineGenerator<int> Integer()
    {
      return new SimpleValueGenerator<int>();
    }

    public static InlineGenerator<double> Double()
    {
      return new SimpleValueGenerator<double>();
    }

    public static InlineGenerator<long> Long()
    {
      return new SimpleValueGenerator<long>();
    }

    public static InlineGenerator<ulong> UnsignedLong()
    {
      return new SimpleValueGenerator<ulong>();
    }

    public static InlineGenerator<byte> Byte()
    {
      return new SimpleValueGenerator<byte>();
    }

    public static InlineGenerator<decimal> Decimal()
    {
      return new SimpleValueGenerator<decimal>();
    }

    public static InlineGenerator<uint> UnsignedInt()
    {
      return new SimpleValueGenerator<uint>();
    }

    public static InlineGenerator<ushort> UnsignedShort()
    {
      return new SimpleValueGenerator<ushort>();
    }

    public static InlineGenerator<short> Short()
    {
      return new SimpleValueGenerator<short>();
    }

    public static InlineGenerator<int> IntegerFromSequence(int startingValue, int step)
    {
      return new IntegerFromSequenceGenerator(startingValue, step, InlineGenerators.Integer());
    }

    public static InlineGenerator<byte> Digit()
    {
      return new DigitGenerator();
    }

    public static InlineGenerator<byte> PositiveDigit()
    {
      return new PositiveDigitGenerator(InlineGenerators.Digit());
    }

    public static InlineGenerator<int> IntegerDivisibleBy(int quotient)
    {
      return new IntegerDivisableByGenerator(quotient);
    }

    public static InlineGenerator<int> IntegerNotDivisibleBy(int quotient)
    {
      return new IntegerNotDivisibleByGenerator(quotient);
    }

    public static InlineGenerator<int> IntegerWithExactDigitCount(int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<int>(NumericTraits.Integer(), digitsCount);
    }

    public static InlineGenerator<long> LongWithExactDigitCount(int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<long>(NumericTraits.Long(), digitsCount);
    }

    public static InlineGenerator<uint> UnsignedIntWithExactDigitCount(int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<uint>(NumericTraits.UnsignedInteger(), digitsCount);
    }

    public static InlineGenerator<ulong> UnsignedLongWithExactDigitCount(int digitsCount)
    {
      return new NumberWithExactDigitNumberGenerator<ulong>(NumericTraits.UnsignedLong(), digitsCount);
    }

    public static InlineGenerator<string> IpString()
    {
      return new IpStringGenerator();
    }

    public static PortNumberGenerator Port()
    {
      return new PortNumberGenerator();
    }

    public static InlineGenerator<T> Substitute<T>() where T : class
    {
      return new SubstituteGenerator<T>();
    }

    public static SimpleInstanceGenerator<Func<T>> Func<T>()
    {
      return new SimpleInstanceGenerator<Func<T>>();
    }

    public static SimpleInstanceGenerator<Func<T1, T2>> Func<T1, T2>()
    {
      return new SimpleInstanceGenerator<Func<T1, T2>>();
    }

    public static SimpleInstanceGenerator<Func<T1, T2, T3>> Func<T1, T2, T3>()
    {
      return new SimpleInstanceGenerator<Func<T1, T2, T3>>();
    }

    public static SimpleInstanceGenerator<Func<T1, T2, T3, T4>> Func<T1, T2, T3, T4>()
    {
      return new SimpleInstanceGenerator<Func<T1, T2, T3, T4>>();
    }

    public static SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5>> Func<T1, T2, T3, T4, T5>()
    {
      return new SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5>>();
    }

    public static SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5, T6>> Func<T1, T2, T3, T4, T5, T6>()
    {
      return new SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5, T6>>();
    }

    public static SimpleInstanceGenerator<Action> Action()
    {
      return new SimpleInstanceGenerator<Action>();
    }

    public static SimpleInstanceGenerator<Action<T>> Action<T>()
    {
      return new SimpleInstanceGenerator<Action<T>>();
    }

    public static SimpleInstanceGenerator<Action<T1, T2>> Action<T1, T2>()
    {
      return new SimpleInstanceGenerator<Action<T1, T2>>();
    }

    public static SimpleInstanceGenerator<Action<T1, T2, T3>> Action<T1, T2, T3>()
    {
      return new SimpleInstanceGenerator<Action<T1, T2, T3>>();
    }

    public static SimpleInstanceGenerator<Action<T1, T2, T3, T4>> Action<T1, T2, T3, T4>()
    {
      return new SimpleInstanceGenerator<Action<T1, T2, T3, T4>>();
    }

    public static SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5>> Action<T1, T2, T3, T4, T5>()
    {
      return new SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5>>();
    }

    public static SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5, T6>> Action<T1, T2, T3, T4, T5, T6>()
    {
      return new SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5, T6>>();
    }

    public static NotStartedTaskGenerator NotStartedTask()
    {
      return new NotStartedTaskGenerator();
    }

    public static NotStartedTaskGenerator<T> NotStartedTask<T>()
    {
      return new NotStartedTaskGenerator<T>();
    }

    public static StartedTaskGenerator<T> StartedTask<T>()
    {
      return new StartedTaskGenerator<T>();
    }
  }
}