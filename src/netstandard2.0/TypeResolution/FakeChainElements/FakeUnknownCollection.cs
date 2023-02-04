using System;
using Core.NullableReferenceTypesExtensions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeUnknownCollection : IResolution
{
  public bool AppliesTo(Type type)
  {
    var smartType = SmartType.For(type);
    return smartType.IsConcrete() &&
           MutableCollectionFiller.IsSupported(type) && 
           smartType.HasPublicParameterlessConstructor();

  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type typeOfCollection)
  {
    var collectionInstance = Activator.CreateInstance(typeOfCollection).OrThrow();
    MutableCollectionFiller.Fill(instanceGenerator, request, collectionInstance);
    return collectionInstance;
  }
}
