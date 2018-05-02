using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Generators;

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

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>> SortedList<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsSortedList();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedList<TKey, TValue>> SortedList<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsSortedList();
    }

    public static EnumerableConversion<T, ISet<T>> Set<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<T, ISet<T>> Set<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSet();
    }

    public static EnumerableConversion<T, SortedSet<T>> SortedSet<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsSortedSet();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<T, SortedSet<T>> SortedSet<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsSortedSet();
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> Dictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsReadOnlyDictionary();
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>> ReadOnlyDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length).AsReadOnlyDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>> Dictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many).AsDictionary();
    }


    public static EnumerableConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsConcurrentDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<KeyValuePair<TKey, TValue>, ConcurrentDictionary<TKey, TValue>> ConcurrentDictionary<TKey, TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsConcurrentDictionary();
    }

    public static EnumerableConversion<T, ConcurrentStack<T>> ConcurrentStack<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentStack();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<T, ConcurrentStack<T>> ConcurrentStack<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentStack();
    }

    public static EnumerableConversion<T, ConcurrentQueue<T>> ConcurrentQueue<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentQueue();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<T, ConcurrentQueue<T>> ConcurrentQueue<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentQueue();
    }

    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> SortedDictionary<TKey,
      TValue>(int length)
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(length)
        .AsSortedDictionary();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<KeyValuePair<TKey, TValue>, SortedDictionary<TKey, TValue>> SortedDictionary<TKey,
      TValue>()
    {
      return new EnumerableGenerator<KeyValuePair<TKey, TValue>>(AllGenerator.Many)
        .AsSortedDictionary();
    }

    public static EnumerableConversion<T, ConcurrentBag<T>> ConcurrentBag<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsConcurrentBag();
    }

    // Used by reflection
    // public API
    // ReSharper disable once UnusedMember.Global
    public static EnumerableConversion<T, ConcurrentBag<T>> ConcurrentBag<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsConcurrentBag();
    }

    public static EnumeratorGenerator<T> Enumerator<T>()
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

    public static InlineGenerator<IReadOnlyList<T>> ReadOnlyList<T>()
    {
      return new EnumerableGenerator<T>(AllGenerator.Many).AsReadOnlyList();
    }

    public static InlineGenerator<IReadOnlyList<T>> ReadOnlyList<T>(int length)
    {
      return new EnumerableGenerator<T>(length).AsReadOnlyList();
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

    public static SimpleValueGenerator<string> String()
    {
      return new SimpleValueGenerator<string>();
    }

    public static SeededValueGenerator<string> SeededString(string seed)
    {
      return new SeededValueGenerator<string>(seed);
    }

    public static ValueConversion<string, string> LowercaseString()
    {
      return new ValueConversion<string, string>(InlineGenerators.String(), s => s.ToLowerInvariant());
    }

    public static ValueConversion<string, string> UppercaseString()
    {
      return new ValueConversion<string, string>(InlineGenerators.String(), s => s.ToUpperInvariant());
    }

    public static InlineGenerator<string> StringMatching(string pattern)
    {
      return new StringMatchingRegexGenerator(pattern);
    }

    public static StringFromCharsGenerator AlphaString(int maxLength)
    {
      return new StringFromCharsGenerator(maxLength, InlineGenerators.AlphaChar());
    }

    public static ValueConversion<string, string> LowercaseAlphaString()
    {
      return new ValueConversion<string, string>(
        AlphaString(Guid.NewGuid().ToString().Length), s => s.ToLowerInvariant());
    }

    public static ValueConversion<string, string> UppercaseAlphaString()
    {
      return new ValueConversion<string, string>(
        AlphaString(Guid.NewGuid().ToString().Length), s => s.ToUpperInvariant());
    }

    public static InlineGenerator<string> String(int length)
    {
      return new StringOfLengthGenerator(length, InlineGenerators.String());
    }

    public static IdentifierStringGenerator Identifier()
    {
      return new IdentifierStringGenerator(InlineGenerators.DigitChar(), InlineGenerators.AlphaChar());
    }

    public static InlineGenerator<string> UrlString()
    {
      return new ValueConversion<Uri, string>(new SimpleValueGenerator<Uri>(), u => u.ToString());
    }
  }
}