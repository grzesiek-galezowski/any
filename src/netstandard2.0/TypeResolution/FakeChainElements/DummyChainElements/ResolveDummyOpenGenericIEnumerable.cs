using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter.Xml;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyOpenGenericIEnumerable : IResolution
{
  private readonly IEmptyCollectionInstantiation _emptyCollectionInstantiation;

  public ResolveDummyOpenGenericIEnumerable(
    IEmptyCollectionInstantiation emptyCollectionInstantiation)
  {
    _emptyCollectionInstantiation = emptyCollectionInstantiation;
  }

  public object Apply(InstanceGenerator allGenerator, GenerationRequest generationRequest, Type type)
  {
    return _emptyCollectionInstantiation.EmptyEnumerableOf(type.GetCollectionItemType());
  }

  public bool AppliesTo(Type type)
  {
    return SmartType.For(type).IsOpenGeneric(typeof(IEnumerable<>));
  }
}
