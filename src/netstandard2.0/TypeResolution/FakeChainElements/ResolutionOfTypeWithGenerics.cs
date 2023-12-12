using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.ResolutionOfGenericTypes;

namespace TddXt.TypeResolution.FakeChainElements;

public class ResolutionOfTypeWithGenerics(
  FactoryForInstancesOfGenericTypes factoryForInstancesOfGenericTypes,
  params Type[] matchingTypes)
  : IResolution
{
  public bool AppliesTo(Type type)
  {
    var result = type.IsGenericType;
    result = result && matchingTypes.Any(matchingType => matchingType == type.GetGenericTypeDefinition());
    return result;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return factoryForInstancesOfGenericTypes.NewInstanceOf(type, instanceGenerator, request);
  }
}
