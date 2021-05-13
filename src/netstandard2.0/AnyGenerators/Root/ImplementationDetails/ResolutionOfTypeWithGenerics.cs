using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class ResolutionOfTypeWithGenerics<T> : IResolution<T>
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

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      return (T)_factoryForInstancesOfGenericTypes.NewInstanceOf(type, instanceGenerator, request);
    }
  }
}
