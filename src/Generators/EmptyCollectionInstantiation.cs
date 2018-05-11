using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace TddEbook.TddToolkit.Generators
{
  public class EmptyCollectionInstantiation
  {
    private Fixture _emptyCollectionGenerator = AutoFixtureConfiguration.InstanceForEmptyCollections();

    public object EmptyEnumerableOf(Type collectionItemType)
    {
      return new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<MyEnumerable>(
        collectionItemType,
        nameof(MyEnumerable.Empty));
    }

    public T CreateCollectionPassedAsGenericType<T>()
    {
      return _emptyCollectionGenerator.Create<T>();
    }

    public class MyEnumerable
    {
      public static IEnumerable<T> Empty<T>()
      {
        return Enumerable.Empty<T>();
      }
    }
  }

}