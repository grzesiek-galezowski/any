using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.AutoFixtureWrapper;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  public class EmptyCollectionInstantiation
  {
    private readonly FixtureWrapper _fixtureWrapper;

    public EmptyCollectionInstantiation()
    {
      _fixtureWrapper = FixtureWrapper.InstanceForEmptyCollections();
    }

    public object EmptyEnumerableOf(Type collectionItemType)
    {
      return new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<MyEnumerable>(
        collectionItemType,
        nameof(MyEnumerable.Empty));
    }

    public T CreateCollectionPassedAsGenericType<T>()
    {
      return _fixtureWrapper.Create<T>();
    }

    private class MyEnumerable
    {
      public static IEnumerable<T> Empty<T>()
      {
        return Enumerable.Empty<T>();
      }
    }
  }

}