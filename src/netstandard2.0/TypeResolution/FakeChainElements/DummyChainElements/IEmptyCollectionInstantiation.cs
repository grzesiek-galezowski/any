using System;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public interface IEmptyCollectionInstantiation
{
  object EmptyEnumerableOf(Type collectionItemType);
  object CreateCollectionPassedAsGenericType(Type type);
}