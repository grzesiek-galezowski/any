using System;
using System.Collections.Generic;
using System.Reflection;
using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.TypeResolution.Interfaces;
using TddEbook.TypeReflection;

namespace Generators
{
  public class CollectionGenerator : ICollectionGenerator
  {
    public Dictionary<TKey, TValue> DictionaryWithKeys<TKey, TValue>(IEnumerable<TKey> keys, IInstanceGenerator instanceGenerator)
    {
      var dict = InlineGenerators.Dictionary<TKey, TValue>(0).GenerateInstance(instanceGenerator);

      foreach (var key in keys)
      {
        dict.Add(key, instanceGenerator.InstanceOf<TValue>());
      }

      return dict;
    }

    public object SortedDictionary(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return InlineGenerators.GetByNameAndTypes(nameof(InlineGenerators.SortedDictionary),
        keyType, valueType).GenerateInstance(generator);
    }

    public object List(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.List), type).GenerateInstance(instanceGenerator);
    }

    public object Dictionary(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return InlineGenerators.GetByNameAndTypes(
        nameof(InlineGenerators.Dictionary), keyType, valueType).GenerateInstance(generator);
    }

    public object Set(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.Set), type).GenerateInstance(instanceGenerator);
    }

    public object SortedList(Type keyType, Type valueType, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndTypes(nameof(InlineGenerators.SortedList),
        keyType, valueType).GenerateInstance(instanceGenerator);
    }

    public object SortedSet(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.SortedSet), type).GenerateInstance(instanceGenerator); 
    }

    public object ConcurrentDictionary(Type keyType, Type valueType, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndTypes(
        nameof(InlineGenerators.ConcurrentDictionary), keyType, valueType).GenerateInstance(instanceGenerator);
    }

    public object Array(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.Array), type).GenerateInstance(instanceGenerator); 
    }

    public ICollection<T> AddManyTo<T>(ICollection<T> collection, int many, IInstanceGenerator instanceGenerator)
    {
      return CollectionFiller.FillingCollection(collection, many, instanceGenerator);
    }

    public object Enumerator(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.Enumerator), type).GenerateInstance(instanceGenerator);
    }

    public object ConcurrentStack(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.ConcurrentStack), type).GenerateInstance(instanceGenerator);
    }

    public object ConcurrentQueue(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.ConcurrentQueue), type).GenerateInstance(instanceGenerator);
    }

    public object ConcurrentBag(Type type, IInstanceGenerator instanceGenerator)
    {
      return InlineGenerators.GetByNameAndType(nameof(InlineGenerators.ConcurrentBag), type).GenerateInstance(instanceGenerator);
    }

    public object KeyValuePair(Type keyType, Type valueType, IInstanceGenerator generator)
    {
      return Activator.CreateInstance(
        typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType), generator.Instance(keyType), generator.Instance(valueType)
      );
    }

  }
}