using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class ResolveDummyOpenGenericImplementationOfIEnumerable(
  IEmptyCollectionInstantiation emptyCollectionInstantiation) : IResolution
{
  public bool AppliesTo(Type type)
  {
    return SmartType.For(type).IsImplementationOfOpenGeneric(typeof(IEnumerable<>));
  }

  public object Apply(InstanceGenerator allGenerator, GenerationRequest request, Type type)
  {
    return emptyCollectionInstantiation.CreateCollectionPassedAsGenericType(type);
  }
}
