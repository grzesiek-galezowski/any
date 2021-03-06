﻿using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class ResolutionOfTypeWithGenerics : IResolution
  {
    private readonly FactoryForInstancesOfGenericTypes _factoryForInstancesOfGenericTypes;
    private readonly Type[] _matchingTypes;

    public ResolutionOfTypeWithGenerics(
      FactoryForInstancesOfGenericTypes factoryForInstancesOfGenericTypes,
      params Type[] matchingTypes)
    {
      _factoryForInstancesOfGenericTypes = factoryForInstancesOfGenericTypes;
      _matchingTypes = matchingTypes;
    }

    public bool AppliesTo(Type type)
    {
      var result = type.IsGenericType;
      result = result && _matchingTypes.Any(matchingType => matchingType == type.GetGenericTypeDefinition());
      return result;
    }

    public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return _factoryForInstancesOfGenericTypes.NewInstanceOf(type, instanceGenerator, request);
    }
  }
}
