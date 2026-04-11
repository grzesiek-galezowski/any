using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.TypeReflection;
using TddXt.TypeResolution.FakeChainElements.DummyChainElements;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic.ImplementationDetails;

public class EmptyCollectionInstantiation : IEmptyCollectionInstantiation
{
  private readonly EmptyCollectionFixtureWrapper _fixtureWrapper;

  public EmptyCollectionInstantiation()
  {
    _fixtureWrapper = EmptyCollectionFixtureWrapper.InstanceForEmptyCollections();
  }

  public object EmptyEnumerableOf(Type collectionItemType)
  {
    return new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<MyEnumerable>(
      collectionItemType,
      nameof(MyEnumerable.Empty));
  }

  public object CreateCollectionPassedAsGenericType(Type type)
  {
    return _fixtureWrapper.Create(type);
  }

  private class MyEnumerable
  {
    public static IEnumerable<T> Empty<T>()
    {
      return Enumerable.Empty<T>();
    }
  }
}
