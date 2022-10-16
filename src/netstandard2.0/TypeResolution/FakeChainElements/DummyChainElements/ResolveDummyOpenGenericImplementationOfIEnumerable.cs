using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyOpenGenericImplementationOfIEnumerable : IResolution
{
  private readonly IEmptyCollectionInstantiation _emptyCollectionInstantiation;

  public ResolveDummyOpenGenericImplementationOfIEnumerable(IEmptyCollectionInstantiation emptyCollectionInstantiation)
  {
    _emptyCollectionInstantiation = emptyCollectionInstantiation;
  }

  public bool AppliesTo(Type type)
  {
    return SmartType.For(type).IsImplementationOfOpenGeneric(typeof(IEnumerable<>));
  }

  public object Apply(InstanceGenerator allGenerator, GenerationRequest request, Type type)
  {
    return _emptyCollectionInstantiation.CreateCollectionPassedAsGenericType(type);
  }
}
