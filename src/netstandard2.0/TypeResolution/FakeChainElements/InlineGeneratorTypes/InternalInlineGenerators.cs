﻿using System;
using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Invokable;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Math;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Network;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Numbers;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Strings;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;

public class InternalInlineGenerators
{
  private static readonly AlphaCharGenerator _alphaCharGenerator;
  private static readonly DigitCharGenerator _digitCharGenerator;
  private static readonly ValueConversion<char, char> _lowerCaseAlphaChar;
  private static readonly ValueConversion<char, char> _upperCaseAlphaChar;
  private static readonly SimpleInstanceGenerator<char> _simpleValueGenerator;
  private static readonly SimpleInstanceGenerator<string> _stringGenerator;
  private static readonly ValueConversion<string, string> _lowercaseString;
  private static readonly ValueConversion<string, string> _uppercaseString;
  private static readonly ValueConversion<string, string> _lowercaseAlphaString;
  private static readonly ValueConversion<string, string> _uppercaseAlphaString;
  private static readonly IdentifierStringGenerator _identifierStringGenerator;
  private static readonly ValueConversion<Uri, string> _uriStringGenerator;
  private static readonly SimpleInstanceGenerator<Guid> _guid;
  private static readonly SimpleInstanceGenerator<Uri> _uriGenerator;
  private static readonly SimpleInstanceGenerator<int> _intGenerator;
  private static readonly DigitGenerator _digitGenerator;
  private static readonly PositiveDigitGenerator _positiveDigitGenerator;
  private static readonly IpStringGenerator _ipStringGenerator;
  private static readonly PortNumberGenerator _portNumberGenerator;
  private static readonly SimpleInstanceGenerator<Action> _actionGenerator;
  private static readonly SimpleInstanceGenerator<IPAddress> _ipAddressGenerator;
  private static readonly SimpleInstanceGenerator<DateTime> _dateTimeGenerator;
  private static readonly SimpleInstanceGenerator<TimeSpan> _timeSpanGenerator;
  private static readonly SimpleInstanceGenerator<bool> _boolGenerator;
  private static readonly SimpleInstanceGenerator<object> _objectGenerator;
  private static readonly SimpleInstanceGenerator<MethodInfo> _methodInfoGenerator;
  private static readonly SimpleInstanceGenerator<Type> _typeGenerator;
  private static readonly SimpleInstanceGenerator<Exception> _exceptionGenerator;

  static InternalInlineGenerators()
  {
    _alphaCharGenerator = new AlphaCharGenerator();
    _digitCharGenerator = new DigitCharGenerator();
    _lowerCaseAlphaChar = _alphaCharGenerator.AsLowerCase();
    _upperCaseAlphaChar = _alphaCharGenerator.AsUpperCase();
    _simpleValueGenerator = new SimpleInstanceGenerator<char>();
    _stringGenerator = new SimpleInstanceGenerator<string>();
    _lowercaseString = new ValueConversion<string, string>(_stringGenerator, s => s.ToLowerInvariant());
    _uppercaseString = new ValueConversion<string, string>(_stringGenerator, s => s.ToUpperInvariant());
    _lowercaseAlphaString = new ValueConversion<string, string>(
      AlphaString(System.Guid.NewGuid().ToString().Length), s => s.ToLowerInvariant());
    _uppercaseAlphaString = new ValueConversion<string, string>(
      AlphaString(System.Guid.NewGuid().ToString().Length), s => s.ToUpperInvariant());
    _identifierStringGenerator = new IdentifierStringGenerator(_digitCharGenerator, _alphaCharGenerator);
    _uriGenerator = new SimpleInstanceGenerator<Uri>();
    _uriStringGenerator = new ValueConversion<Uri, string>(_uriGenerator, u => u.ToString());
    _guid = new SimpleInstanceGenerator<Guid>();
    _intGenerator = new SimpleInstanceGenerator<int>();
    _digitGenerator = new DigitGenerator();
    _positiveDigitGenerator = new PositiveDigitGenerator(_digitGenerator);
    _ipStringGenerator = new IpStringGenerator();
    _portNumberGenerator = new PortNumberGenerator();
    _actionGenerator = new SimpleInstanceGenerator<Action>();
    _ipAddressGenerator = new SimpleInstanceGenerator<IPAddress>();
    _dateTimeGenerator = new SimpleInstanceGenerator<DateTime>();
    _timeSpanGenerator = new SimpleInstanceGenerator<TimeSpan>();
    _boolGenerator = new SimpleInstanceGenerator<bool>();
    _objectGenerator = new SimpleInstanceGenerator<object>();
    _methodInfoGenerator = new SimpleInstanceGenerator<MethodInfo>();
    _typeGenerator = new SimpleInstanceGenerator<Type>();
    _exceptionGenerator = new SimpleInstanceGenerator<Exception>();
  }

  public static InlineGenerator<IEnumerable<T>> EnumerableWith<T>(IEnumerable<T> included)
  {
    return new InclusiveEnumerableGenerator<T>(included);
  }

  public static InlineGenerator<IEnumerable<T>> Enumerable<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest());
  }

  public static InlineGenerator<IEnumerable<T>> Enumerable<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length));
  }

  public static InlineGenerator<IEnumerable<T>> EnumerableWithout<T>(T[] excluded)
  {
    return new ExclusiveEnumerableGenerator<T>(excluded, Configuration.Many);
  }

  public static InlineGenerator<T[]> Array<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsArray();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<T[]> Array<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsArray();
  }

  public static InlineGenerator<T[]> ArrayWithout<T>(T[] excluded)
  {
    return EnumerableWithout(excluded).AsArray();
  }

  public static InlineGenerator<T[]> ArrayWith<T>(T[] included)
  {
    return new InclusiveEnumerableGenerator<T>(included).AsArray();
  }

  public static InlineGenerator<List<T>> List<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsList();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<List<T>> List<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsList();
  }

  public static InlineGenerator<List<T>> ListWithout<T>(T[] excluded)
  {
    return EnumerableWithout(excluded).AsList();
  }

  public static InlineGenerator<List<T>> ListWith<T>(T[] included)
  {
    return new InclusiveEnumerableGenerator<T>(included).AsList();
  }

  public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>(int length) where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromConstant(length)).AsSortedList();
  }

  public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(
      ManyStrategies.FromRequest()).AsSortedList();
  }

  public static InlineGenerator<ISet<T>> Set<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsSet();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ISet<T>> Set<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(Configuration.Many)).AsSet();
  }

  public static InlineGenerator<SortedSet<T>> SortedSet<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsSortedSet();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<SortedSet<T>> SortedSet<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsSortedSet();
  }

  public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(int length) where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromConstant(length)).AsDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest()).AsReadOnlyDictionary();
  }

  public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(int length) where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromConstant(length)).AsReadOnlyDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest()).AsDictionary();
  }

  public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(int length) where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromConstant(length))
      .AsConcurrentDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest())
      .AsConcurrentDictionary();
  }

  public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsConcurrentStack();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsConcurrentStack();
  }

  public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsConcurrentQueue();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsConcurrentQueue();
  }

  public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey, TValue>(int length) where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromConstant(length))
      .AsSortedDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest())
      .AsSortedDictionary();
  }

  public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>(int length)
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromConstant(length)).AsConcurrentBag();
  }

  public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsConcurrentBag();
  }

  public static InlineGenerator<IEnumerator<T>> Enumerator<T>()
  {
    return new EnumeratorGenerator<T>(ManyStrategies.FromRequest());
  }

  public static InlineGenerator<object> GetByNameAndType(string methodName, Type type)
  {
    return ObjectAdapter.For<InternalInlineGenerators>(methodName, type);
  }

  public static InlineGenerator<object> GetByNameAndTypes(string methodName, Type type1, Type type2)
  {
    return ObjectAdapter.For<InternalInlineGenerators>(methodName, type1, type2);
  }

  public static InlineGenerator<T> From<T>(T[] possibleValues)
  {
    return new GeneratorByPickingFromSpecifiedSetOfValues<T>(possibleValues);
  }

  public static InlineGenerator<Dictionary<TKey, TValue>> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys) where TKey : notnull
  {
    return new DictionaryWithKeysGenerator<TKey, TValue>(keys);
  }

  public static InlineGenerator<KeyValuePair<TKey, TValue>> KeyValuePair<TKey, TValue>()
  {
    return new KeyValuePairGenerator<TKey, TValue>();
  }

  public static InlineGenerator<char> AlphaChar()
  {
    return _alphaCharGenerator;
  }

  public static InlineGenerator<char> DigitChar()
  {
    return _digitCharGenerator;
  }

  public static InlineGenerator<char> LowerCaseAlphaChar()
  {
    return _lowerCaseAlphaChar;
  }

  public static InlineGenerator<char> UpperCaseAlphaChar()
  {
    return _upperCaseAlphaChar;
  }

  public static InlineGenerator<char> Char()
  {
    return _simpleValueGenerator;
  }

  public static InlineGenerator<string> NumericString(int digitsCount)
  {
    return new StringMatchingRegexGenerator("[1-9][0-9]{" + (digitsCount - 1) + "}");
  }

  public static InlineGenerator<string> String()
  {
    return _stringGenerator;
  }

  public static InlineGenerator<string> SeededString(string seed)
  {
    return new SeededStringGenerator(seed);
  }

  public static InlineGenerator<string> LowercaseString()
  {
    return _lowercaseString;
  }

  public static InlineGenerator<string> UppercaseString()
  {
    return _uppercaseString;
  }

  public static InlineGenerator<string> StringMatching(string pattern)
  {
    return new StringMatchingRegexGenerator(pattern);
  }

  public static InlineGenerator<string> AlphaString(int length)
  {
    return new StringFromCharsGenerator(length, AlphaChar());
  }

  public static InlineGenerator<string> LowercaseAlphaString()
  {
    return _lowercaseAlphaString;
  }

  public static InlineGenerator<string> UppercaseAlphaString()
  {
    return _uppercaseAlphaString;
  }

  public static InlineGenerator<string> String(int length)
  {
    return new StringOfLengthGenerator(length, String());
  }

  public static InlineGenerator<string> Identifier()
  {
    return _identifierStringGenerator;
  }

  public static InlineGenerator<string> UrlString()
  {
    return _uriStringGenerator;
  }

  public static InlineGenerator<string> StringNotContaining(string[] excludedSubstrings)
  {
    return new StringNotContainingGenerator(excludedSubstrings, String());
  }

  public static InlineGenerator<string> StringNotContaining<T>(T[] excludedObjects)
  {
    return StringNotContaining((from obj in new[] { new[] { excludedObjects } } select obj.ToString()).ToArray());
  }

  public static InlineGenerator<T> ValueOtherThan<T>(T[] excluded)
  {
    return new SimpleValueOtherThanGenerator<T>(excluded);
  }

  public static InlineGenerator<string> StringContaining(string str)
  {
    return new AggregatingGenerator<string>(
      string.Empty,
      (current, next) => current + next,
      _stringGenerator,
      new FixedValueGenerator<string>(str),
      _stringGenerator);
  }

  public static InlineGenerator<Guid> Guid()
  {
    return _guid;
  }

  public static InlineGenerator<Uri> Uri()
  {
    return _uriGenerator;
  }

  public static InlineGenerator<int> IntegerFromSequence(int startingValue, int step)
  {
    return new IntegerFromSequenceGenerator(startingValue, step, _intGenerator);
  }

  public static InlineGenerator<byte> Digit()
  {
    return _digitGenerator;
  }

  public static InlineGenerator<byte> PositiveDigit()
  {
    return _positiveDigitGenerator;
  }

  public static InlineGenerator<int> IntegerDivisibleBy(int quotient)
  {
    return new IntegerDivisibleByGenerator(quotient);
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

  public static InlineGenerator<int> IntegerInRange(int min, int max)
  {
    return new NumberInRangeGenerator<int>(min, max, n => (int)n);
  }

  public static InlineGenerator<short> ShortInRange(short min, short max)
  {
    return new NumberInRangeGenerator<short>(min, max, n => (short)n);
  }

  public static InlineGenerator<ushort> UnsignedShortInRange(ushort min, ushort max)
  {
    return new NumberInRangeGenerator<ushort>(min, max, n => (ushort)n);
  }

  public static InlineGenerator<long> LongInRange(long min, long max)
  {
    return new NumberInRangeGenerator<long>(min, max, n => (long)n);
  }

  public static InlineGenerator<uint> UnsignedIntInRange(uint min, uint max)
  {
    return new NumberInRangeGenerator<uint>(min, max, n => (uint)n);
  }

  public static InlineGenerator<ulong> UnsignedLongInRange(ulong min, ulong max)
  {
    return new NumberInRangeGenerator<ulong>(min, max, n => (ulong)n);
  }

  public static InlineGenerator<decimal> DecimalInRange(decimal min, decimal max)
  {
    return new NumberInRangeGenerator<decimal>(new BigInteger(min), new BigInteger(max), n => (decimal)n);
  }

  public static InlineGenerator<byte> ByteInRange(byte min, byte max)
  {
    return new NumberInRangeGenerator<byte>(min, max, n => (byte)n);
  }

  public static InlineGenerator<sbyte> SignedByteInRange(sbyte min, sbyte max)
  {
    return new NumberInRangeGenerator<sbyte>(min, max, n => (sbyte)n);
  }

  public static InlineGenerator<string> IpString()
  {
    return _ipStringGenerator;
  }

  public static PortNumberGenerator Port()
  {
    return _portNumberGenerator;
  }

  public static InlineGenerator<Func<T>> Func<T>()
  {
    return new SimpleInstanceGenerator<Func<T>>();
  }

  public static InlineGenerator<Func<T1, T2>> Func<T1, T2>()
  {
    return new SimpleInstanceGenerator<Func<T1, T2>>();
  }

  public static InlineGenerator<Func<T1, T2, T3>> Func<T1, T2, T3>()
  {
    return new SimpleInstanceGenerator<Func<T1, T2, T3>>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4>> Func<T1, T2, T3, T4>()
  {
    return new SimpleInstanceGenerator<Func<T1, T2, T3, T4>>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4, T5>> Func<T1, T2, T3, T4, T5>()
  {
    return new SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5>>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4, T5, T6>> Func<T1, T2, T3, T4, T5, T6>()
  {
    return new SimpleInstanceGenerator<Func<T1, T2, T3, T4, T5, T6>>();
  }

  public static InlineGenerator<Action> Action()
  {
    return _actionGenerator;
  }

  public static InlineGenerator<Action<T>> Action<T>()
  {
    return new SimpleInstanceGenerator<Action<T>>();
  }

  public static InlineGenerator<Action<T1, T2>> Action<T1, T2>()
  {
    return new SimpleInstanceGenerator<Action<T1, T2>>();
  }

  public static InlineGenerator<Action<T1, T2, T3>> Action<T1, T2, T3>()
  {
    return new SimpleInstanceGenerator<Action<T1, T2, T3>>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4>> Action<T1, T2, T3, T4>()
  {
    return new SimpleInstanceGenerator<Action<T1, T2, T3, T4>>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4, T5>> Action<T1, T2, T3, T4, T5>()
  {
    return new SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5>>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4, T5, T6>> Action<T1, T2, T3, T4, T5, T6>()
  {
    return new SimpleInstanceGenerator<Action<T1, T2, T3, T4, T5, T6>>();
  }

  public static InlineGenerator<Task<T>> StartedTask<T>()
  {
    return new StartedTaskGenerator<T>();
  }

  public static InlineGenerator<Task> StartedTask()
  {
    return new StartedTaskGenerator();
  }

  public static InlineGenerator<T> Exploding<T>() where T : class
  {
    return new ExplodingInstanceGenerator<T>();
  }

  public static InlineGenerator<IPAddress> IpAddress()
  {
    return _ipAddressGenerator;
  }

  public static InlineGenerator<DateTime> DateTime()
  {
    return _dateTimeGenerator;
  }

  public static InlineGenerator<TimeSpan> TimeSpan()
  {
    return _timeSpanGenerator;
  }

  public static InlineGenerator<bool> Boolean()
  {
    return _boolGenerator;
  }

  public static InlineGenerator<object> Object()
  {
    return _objectGenerator;
  }

  public static InlineGenerator<MethodInfo> MethodInfo()
  {
    return _methodInfoGenerator;
  }

  public static InlineGenerator<Type> Type()
  {
    return _typeGenerator;
  }

  public static InlineGenerator<Exception> Exception()
  {
    return _exceptionGenerator;
  }

  public static InlineGenerator<T> OtherThan<T>(T[] omittedValues)
  {
    return new SimpleInstanceOtherThanGenerator<T>(omittedValues);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableArray<T>> ImmutableArray<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableArray();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableList<T>> ImmutableList<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableList();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableHashSet<T>> ImmutableHashSet<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableHashSet();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableSortedSet<T>> ImmutableSortedSet<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableSortedSet();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableDictionary<TKey, TValue>> ImmutableDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest()).AsImmutableDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableSortedDictionary<TKey, TValue>> ImmutableSortedDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest()).AsImmutableSortedDictionary();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableQueue<T>> ImmutableQueue<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableQueue();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableStack<T>> ImmutableStack<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsImmutableStack();
  }

  public static InlineGenerator<Lazy<T>> Lazy<T>()
  {
    return new LazyGenerator<T>();
  }

  public static InlineGenerator<T?> Nullable<T>() where T : struct
  {
    return new NullableGenerator<T>();
  }

  public static InlineGenerator<FrozenSet<T>> FrozenSet<T>()
  {
    return new EnumerableGenerator<T>(ManyStrategies.FromRequest()).AsFrozenSet<T>();
  }

  public static InlineGenerator<FrozenDictionary<TKey, TValue>> FrozenDictionary<TKey, TValue>() where TKey : notnull
  {
    return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(ManyStrategies.FromRequest()).AsFrozenDictionary();
  }
}
