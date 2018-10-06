using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class SpecialCasesOfResolutions<T> : ISpecialCasesOfResolutions<T>
  {
    public IResolution<T> CreateResolutionOfKeyValuePair()
    {
      return CreateResolutionOf2GenericType(nameof(InlineGenerators.KeyValuePair), 
        typeof(KeyValuePair<,>)
        );
    }

    public IResolution<T> CreateResolutionOf2GenericType(string className, params Type[] matchingTypes)
    {
      var factoryMethod = new Func<Type, Type, InstanceGenerator, GenerationTrace, object>((type1, type2, instanceGenerator, trace) => 
        InlineGenerators.GetByNameAndTypes(className, type1, type2)
          .GenerateInstance(instanceGenerator, trace));

      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          factoryMethod),
        matchingTypes);
    }


    public IResolution<T> CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes)
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (type, generator, trace) => InlineGenerators.GetByNameAndType(resolvedTypeName, type)
            .GenerateInstance(generator, trace)),
        genericTypes);
    }

    public IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfArrays<T>();
    }
  }
}