using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.Interfaces
{
  public interface ICollectionGenerator
  {
    object Dictionary(Type keyType, Type valueType, IInstanceGenerator generator);
    object SortedDictionary(Type keyType, Type valueType, IInstanceGenerator generator);
    object List(Type type, IInstanceGenerator instanceGenerator);
    object Set(Type type, IInstanceGenerator instanceGenerator);
    object SortedList(Type keyType, Type valueType, IInstanceGenerator instanceGenerator);
    object SortedSet(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentDictionary(Type keyType, Type valueType, IInstanceGenerator instanceGenerator);
    object Array(Type type, IInstanceGenerator instanceGenerator);
    object Enumerator(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentStack(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentQueue(Type type, IInstanceGenerator instanceGenerator);
    object ConcurrentBag(Type type, IInstanceGenerator instanceGenerator);
    object KeyValuePair(Type keyType, Type valueType, IInstanceGenerator generator);
  }
}