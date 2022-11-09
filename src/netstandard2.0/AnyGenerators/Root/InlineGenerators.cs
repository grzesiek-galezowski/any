using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Network;

namespace TddXt.AnyGenerators.Root;

public class InlineGenerators
{
  public static InlineGenerator<IEnumerable<T>> EnumerableWith<T>(IEnumerable<T> included)
  {
    return InternalInlineGenerators.EnumerableWith(included);
  }

  public static InlineGenerator<IEnumerable<T>> Enumerable<T>()
  {
    return InternalInlineGenerators.Enumerable<T>();
  }

  public static InlineGenerator<IEnumerable<T>> Enumerable<T>(int length)
  {
    return InternalInlineGenerators.Enumerable<T>(length);
  }

  public static InlineGenerator<IEnumerable<T>> EnumerableWithout<T>(T[] excluded)
  {
    return InternalInlineGenerators.EnumerableWithout(excluded);
  }

  public static InlineGenerator<T[]> Array<T>(int length)
  {
    return InternalInlineGenerators.Array<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<T[]> Array<T>()
  {
    return InternalInlineGenerators.Array<T>();
  }

  public static InlineGenerator<T[]> ArrayWithout<T>(T[] excluded)
  {
    return InternalInlineGenerators.ArrayWithout(excluded);
  }

  public static InlineGenerator<T[]> ArrayWith<T>(T[] included)
  {
    return InternalInlineGenerators.ArrayWith(included);
  }

  public static InlineGenerator<List<T>> List<T>(int length)
  {
    return InternalInlineGenerators.List<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<List<T>> List<T>()
  {
    return InternalInlineGenerators.List<T>();
  }

  public static InlineGenerator<List<T>> ListWithout<T>(T[] excluded)
  {
    return InternalInlineGenerators.ListWithout(excluded);
  }

  public static InlineGenerator<List<T>> ListWith<T>(T[] included)
  {
    return InternalInlineGenerators.ListWith(included);
  }

  public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>(int length)
  {
    return InternalInlineGenerators.SortedList<TKey, TValue>(length);
  }

  public static InlineGenerator<SortedList<TKey, TValue>> SortedList<TKey, TValue>()
  {
    return InternalInlineGenerators.SortedList<TKey, TValue>();
  }

  public static InlineGenerator<ISet<T>> Set<T>(int length)
  {
    return InternalInlineGenerators.Set<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ISet<T>> Set<T>()
  {
    return InternalInlineGenerators.Set<T>();
  }

  public static InlineGenerator<SortedSet<T>> SortedSet<T>(int length)
  {
    return InternalInlineGenerators.SortedSet<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<SortedSet<T>> SortedSet<T>()
  {
    return InternalInlineGenerators.SortedSet<T>();
  }

  public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(int length)
  {
    return InternalInlineGenerators.Dictionary<TKey, TValue>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>()
  {
    return InternalInlineGenerators.ReadOnlyDictionary<TKey, TValue>();
  }

  public static InlineGenerator<IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(int length)
  {
    return InternalInlineGenerators.ReadOnlyDictionary<TKey, TValue>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<Dictionary<TKey, TValue>> Dictionary<TKey, TValue>()
  {
    return InternalInlineGenerators.Dictionary<TKey, TValue>();
  }


  public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(int length)
  {
    return InternalInlineGenerators.ConcurrentDictionary<TKey, TValue>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>()
  {
    return InternalInlineGenerators.ConcurrentDictionary<TKey, TValue>();
  }

  public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>(int length)
  {
    return InternalInlineGenerators.ConcurrentStack<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentStack<T>> ConcurrentStack<T>()
  {
    return InternalInlineGenerators.ConcurrentStack<T>();
  }

  public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>(int length)
  {
    return InternalInlineGenerators.ConcurrentQueue<T>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ConcurrentQueue<T>> ConcurrentQueue<T>()
  {
    return InternalInlineGenerators.ConcurrentQueue<T>();
  }

  public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey,TValue>(int length)
  {
    return InternalInlineGenerators.SortedDictionary<TKey, TValue>(length);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<SortedDictionary<TKey, TValue>> SortedDictionary<TKey,TValue>()
  {
    return InternalInlineGenerators.SortedDictionary<TKey, TValue>();
  }

  public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>(int length)
  {
    return InternalInlineGenerators.ConcurrentBag<T>(length);
  }

  public static InlineGenerator<ConcurrentBag<T>> ConcurrentBag<T>()
  {
    return InternalInlineGenerators.ConcurrentBag<T>();
  }

  public static InlineGenerator<IEnumerator<T>> Enumerator<T>()
  {
    return InternalInlineGenerators.Enumerator<T>();
  }

  public static InlineGenerator<object> GetByNameAndType(string methodName, Type type)
  {
    return InternalInlineGenerators.GetByNameAndType(methodName, type);
  }

  public static InlineGenerator<object> GetByNameAndTypes(string methodName, Type type1, Type type2)
  {
    return InternalInlineGenerators.GetByNameAndTypes(methodName, type1, type2);
  }

  public static InlineGenerator<T> From<T>(T[] possibleValues)
  {
    return InternalInlineGenerators.From(possibleValues);
  }

  public static InlineGenerator<Dictionary<TKey, TValue>> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys)
  {
    return InternalInlineGenerators.DictionaryWithKeys<TKey, TValue>(keys);
  }

  public static InlineGenerator<KeyValuePair<TKey, TValue>> KeyValuePair<TKey, TValue>()
  {
    return InternalInlineGenerators.KeyValuePair<TKey, TValue>();
  }

  public static InlineGenerator<char> AlphaChar()
  {
    return InternalInlineGenerators.AlphaChar();
  }

  public static InlineGenerator<char> DigitChar()
  {
    return InternalInlineGenerators.DigitChar();
  }

  public static InlineGenerator<char> LowerCaseAlphaChar()
  {
    return InternalInlineGenerators.LowerCaseAlphaChar();
  }

  public static InlineGenerator<char> UpperCaseAlphaChar()
  {
    return InternalInlineGenerators.UpperCaseAlphaChar();
  }

  public static InlineGenerator<char> Char()
  {
    return InternalInlineGenerators.Char();
  }

  public static InlineGenerator<string> NumericString(int digitsCount)
  {
    return InternalInlineGenerators.NumericString(digitsCount);
  }

  public static InlineGenerator<string> String()
  {
    return InternalInlineGenerators.String();
  }

  public static InlineGenerator<string> SeededString(string seed)
  {
    return InternalInlineGenerators.SeededString(seed);
  }

  public static InlineGenerator<string> LowercaseString()
  {
    return InternalInlineGenerators.LowercaseString();
  }

  public static InlineGenerator<string> UppercaseString()
  {
    return InternalInlineGenerators.UppercaseString();
  }

  public static InlineGenerator<string> StringMatching(string pattern)
  {
    return InternalInlineGenerators.StringMatching(pattern);
  }

  public static InlineGenerator<string> AlphaString(int length)
  {
    return InternalInlineGenerators.AlphaString(length);
  }

  public static InlineGenerator<string> LowercaseAlphaString()
  {
    return InternalInlineGenerators.LowercaseAlphaString();
  }

  public static InlineGenerator<string> UppercaseAlphaString()
  {
    return InternalInlineGenerators.UppercaseAlphaString();
  }

  public static InlineGenerator<string> String(int length)
  {
    return InternalInlineGenerators.String(length);
  }

  public static InlineGenerator<string> Identifier()
  {
    return InternalInlineGenerators.Identifier();
  }

  public static InlineGenerator<string> UrlString()
  {
    return InternalInlineGenerators.UrlString();
  }

  public static InlineGenerator<string> StringNotContaining(string[] excludedSubstrings)
  {
    return InternalInlineGenerators.StringNotContaining(excludedSubstrings);
  }

  public static InlineGenerator<string> StringNotContaining<T>(T[] excludedObjects)
  {
    return InternalInlineGenerators.StringNotContaining(excludedObjects);
  }

  public static InlineGenerator<T> ValueOtherThan<T>(T[] excluded)
  {
    return InternalInlineGenerators.ValueOtherThan(excluded);
  }

  public static InlineGenerator<string> StringContaining(string str)
  {
    return InternalInlineGenerators.StringContaining(str);
  }

  public static InlineGenerator<Guid> Guid()
  {
    return InternalInlineGenerators.Guid();
  }

  public static InlineGenerator<Uri> Uri()
  {
    return InternalInlineGenerators.Uri();
  }

  public static InlineGenerator<int> Integer()
  {
    return InternalInlineGenerators.Integer();
  }

  public static InlineGenerator<double> Double()
  {
    return InternalInlineGenerators.Double();
  }

  public static InlineGenerator<long> Long()
  {
    return InternalInlineGenerators.Long();
  }

  public static InlineGenerator<ulong> UnsignedLong()
  {
    return InternalInlineGenerators.UnsignedLong();
  }

  public static InlineGenerator<byte> Byte()
  {
    return InternalInlineGenerators.Byte();
  }

  public static InlineGenerator<decimal> Decimal()
  {
    return InternalInlineGenerators.Decimal();
  }

  public static InlineGenerator<uint> UnsignedInt()
  {
    return InternalInlineGenerators.UnsignedInt();
  }

  public static InlineGenerator<ushort> UnsignedShort()
  {
    return InternalInlineGenerators.UnsignedShort();
  }

  public static InlineGenerator<short> Short()
  {
    return InternalInlineGenerators.Short();
  }

  public static InlineGenerator<int> IntegerFromSequence(int startingValue, int step)
  {
    return InternalInlineGenerators.IntegerFromSequence(startingValue, step);
  }

  public static InlineGenerator<byte> Digit()
  {
    return InternalInlineGenerators.Digit();
  }

  public static InlineGenerator<byte> PositiveDigit()
  {
    return InternalInlineGenerators.PositiveDigit();
  }

  public static InlineGenerator<int> IntegerDivisibleBy(int quotient)
  {
    return InternalInlineGenerators.IntegerDivisibleBy(quotient);
  }

  public static InlineGenerator<int> IntegerNotDivisibleBy(int quotient)
  {
    return InternalInlineGenerators.IntegerNotDivisibleBy(quotient);
  }

  public static InlineGenerator<int> IntegerWithExactDigitCount(int digitsCount)
  {
    return InternalInlineGenerators.IntegerWithExactDigitCount(digitsCount);
  }

  public static InlineGenerator<long> LongWithExactDigitCount(int digitsCount)
  {
    return InternalInlineGenerators.LongWithExactDigitCount(digitsCount);
  }

  public static InlineGenerator<uint> UnsignedIntWithExactDigitCount(int digitsCount)
  {
    return InternalInlineGenerators.UnsignedIntWithExactDigitCount(digitsCount);
  }

  public static InlineGenerator<ulong> UnsignedLongWithExactDigitCount(int digitsCount)
  {
    return InternalInlineGenerators.UnsignedLongWithExactDigitCount(digitsCount);
  }

  public static InlineGenerator<string> IpString()
  {
    return InternalInlineGenerators.IpString();
  }

  public static PortNumberGenerator Port()
  {
    return InternalInlineGenerators.Port();
  }

  public static InlineGenerator<Func<T>> Func<T>()
  {
    return InternalInlineGenerators.Func<T>();
  }

  public static InlineGenerator<Func<T1, T2>> Func<T1, T2>()
  {
    return InternalInlineGenerators.Func<T1, T2>();
  }

  public static InlineGenerator<Func<T1, T2, T3>> Func<T1, T2, T3>()
  {
    return InternalInlineGenerators.Func<T1, T2, T3>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4>> Func<T1, T2, T3, T4>()
  {
    return InternalInlineGenerators.Func<T1, T2, T3, T4>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4, T5>> Func<T1, T2, T3, T4, T5>()
  {
    return InternalInlineGenerators.Func<T1, T2, T3, T4, T5>();
  }

  public static InlineGenerator<Func<T1, T2, T3, T4, T5, T6>> Func<T1, T2, T3, T4, T5, T6>()
  {
    return InternalInlineGenerators.Func<T1, T2, T3, T4, T5, T6>();
  }

  public static InlineGenerator<Action> Action()
  {
    return InternalInlineGenerators.Action();
  }

  public static InlineGenerator<Action<T>> Action<T>()
  {
    return InternalInlineGenerators.Action<T>();
  }

  public static InlineGenerator<Action<T1, T2>> Action<T1, T2>()
  {
    return InternalInlineGenerators.Action<T1, T2>();
  }

  public static InlineGenerator<Action<T1, T2, T3>> Action<T1, T2, T3>()
  {
    return InternalInlineGenerators.Action<T1, T2, T3>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4>> Action<T1, T2, T3, T4>()
  {
    return InternalInlineGenerators.Action<T1, T2, T3, T4>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4, T5>> Action<T1, T2, T3, T4, T5>()
  {
    return InternalInlineGenerators.Action<T1, T2, T3, T4, T5>();
  }

  public static InlineGenerator<Action<T1, T2, T3, T4, T5, T6>> Action<T1, T2, T3, T4, T5, T6>()
  {
    return InternalInlineGenerators.Action<T1, T2, T3, T4, T5, T6>();
  }

  public static InlineGenerator<Task> NotStartedTask()
  {
    return InternalInlineGenerators.NotStartedTask();
  }

  public static InlineGenerator<Task<T>> NotStartedTask<T>()
  {
    return InternalInlineGenerators.NotStartedTask<T>();
  }

  public static InlineGenerator<Task<T>> StartedTask<T>()
  {
    return InternalInlineGenerators.StartedTask<T>();
  }

  public static InlineGenerator<Task> StartedTask()
  {
    return InternalInlineGenerators.StartedTask();
  }

  public static InlineGenerator<T> Exploding<T>() where T : class
  {
    return InternalInlineGenerators.Exploding<T>();
  }

  public static InlineGenerator<IPAddress> IpAddress()
  {
    return InternalInlineGenerators.IpAddress();
  }

  public static InlineGenerator<DateTime> DateTime()
  {
    return InternalInlineGenerators.DateTime();
  }

  public static InlineGenerator<TimeSpan> TimeSpan()
  {
    return InternalInlineGenerators.TimeSpan();
  }

  public static InlineGenerator<bool> Boolean()
  {
    return InternalInlineGenerators.Boolean();
  }

  public static InlineGenerator<object> Object()
  {
    return InternalInlineGenerators.Object();
  }

  public static InlineGenerator<MethodInfo> MethodInfo()
  {
    return InternalInlineGenerators.MethodInfo();
  }

  public static InlineGenerator<Type> Type()
  {
    return InternalInlineGenerators.Type();
  }

  public static InlineGenerator<Exception> Exception()
  {
    return InternalInlineGenerators.Exception();
  }

  public static InlineGenerator<T> OtherThan<T>(T[] omittedValues)
  {
    return InternalInlineGenerators.OtherThan(omittedValues);
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableArray<T>> ImmutableArray<T>()
  {
    return InternalInlineGenerators.ImmutableArray<T>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableList<T>> ImmutableList<T>()
  {
    return InternalInlineGenerators.ImmutableList<T>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableHashSet<T>> ImmutableHashSet<T>()
  {
    return InternalInlineGenerators.ImmutableHashSet<T>();
  }
    
  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableSortedSet<T>> ImmutableSortedSet<T>()
  {
    return InternalInlineGenerators.ImmutableSortedSet<T>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableDictionary<T1, T2>> ImmutableDictionary<T1, T2>()
  {
    return InternalInlineGenerators.ImmutableDictionary<T1, T2>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableSortedDictionary<T1, T2>> ImmutableSortedDictionary<T1, T2>()
  {
    return InternalInlineGenerators.ImmutableSortedDictionary<T1, T2>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableQueue<T>> ImmutableQueue<T>()
  {
    return InternalInlineGenerators.ImmutableQueue<T>();
  }

  // Used by reflection
  // public API
  // ReSharper disable once UnusedMember.Global
  public static InlineGenerator<ImmutableStack<T>> ImmutableStack<T>()
  {
    return InternalInlineGenerators.ImmutableStack<T>();
  }

  public static InlineGenerator<Lazy<T>> Lazy<T>()
  {
    return InternalInlineGenerators.Lazy<T>();
  }

  public static InlineGenerator<T?> Nullable<T>() where T : struct
  {
    return InternalInlineGenerators.Nullable<T>();
  }

  public static InlineGenerator<int> IntegerInRange_NotForTestingBoundaries(int min, int max)
  {
    return InternalInlineGenerators.IntegerInRange(min, max);
  }

  public static InlineGenerator<short> ShortInRange_NotForTestingBoundaries(short min, short max)
  {
    return InternalInlineGenerators.ShortInRange(min, max);
  }

  public static InlineGenerator<ushort> UnsignedShortInRange_NotForTestingBoundaries(ushort min, ushort max)
  {
    return InternalInlineGenerators.UnsignedShortInRange(min, max);
  }

  public static InlineGenerator<long> LongInRange_NotForTestingBoundaries(long min, long max)
  {
    return InternalInlineGenerators.LongInRange(min, max);
  }

  public static InlineGenerator<uint> UnsignedIntInRange_NotForTestingBoundaries(uint min, uint max)
  {
    return InternalInlineGenerators.UnsignedIntInRange(min, max);
  }

  public static InlineGenerator<ulong> UnsignedLongInRange_NotForTestingBoundaries(ulong min, ulong max)
  {
    return InternalInlineGenerators.UnsignedLongInRange(min, max);
  }

  public static InlineGenerator<decimal> DecimalInRange_NotForTestingBoundaries(decimal min, decimal max)
  {
    return InternalInlineGenerators.DecimalInRange(min, max);
  }

  public static InlineGenerator<byte> ByteInRange_NotForTestingBoundaries(byte min, byte max)
  {
    return InternalInlineGenerators.ByteInRange(min, max);
  }

  public static InlineGenerator<sbyte> SignedByteInRange_NotForTestingBoundaries(sbyte min, sbyte max)
  {
    return InternalInlineGenerators.SignedByteInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<int> IntegerInRange(int min, int max)
  {
    return InternalInlineGenerators.IntegerInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<short> ShortInRange(short min, short max)
  {
    return InternalInlineGenerators.ShortInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<ushort> UnsignedShortInRange(ushort min, ushort max)
  {
    return InternalInlineGenerators.UnsignedShortInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<long> LongInRange(long min, long max)
  {
    return InternalInlineGenerators.LongInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<uint> UnsignedIntInRange(uint min, uint max)
  {
    return InternalInlineGenerators.UnsignedIntInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<ulong> UnsignedLongInRange(ulong min, ulong max)
  {
    return InternalInlineGenerators.UnsignedLongInRange(min, max);
  }

  [Obsolete("Use the same method but with _NotForTestingBoundaries suffix. " +
            "I added the suffix so that the users are 100% sure that when they " +
            "Cover this is an extension method and use it for testing boundaries," +
            "they are hurting their teams.", true)]
  public static InlineGenerator<decimal> DecimalInRange(decimal min, decimal max)
  {
    return InternalInlineGenerators.DecimalInRange(min, max);
  }

  public static InlineGenerator<byte> ByteInRange(byte min, byte max)
  {
    return InternalInlineGenerators.ByteInRange(min, max);
  }

  public static InlineGenerator<sbyte> SignedByteInRange(sbyte min, sbyte max)
  {
    return InternalInlineGenerators.SignedByteInRange(min, max);
  }
}
