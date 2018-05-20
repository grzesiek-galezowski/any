using System;
using System.Collections.Generic;
using TddXt.AnyGenerators.Generic.ExtensionPoints;
using TypeResolution.FakeChainElements;

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
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith2Generics(
          (type1, type2, arg3) => InlineGenerators.GetByNameAndTypes(className,
            type1, type2).GenerateInstance(arg3)),
        matchingTypes);
    }


    public IResolution<T> CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes)
    {
      return new ResolutionOfTypeWithGenerics<T>(
        new FactoryForInstancesOfGenericTypesWith1Generic(
          (type, generator) => InlineGenerators.GetByNameAndType(resolvedTypeName, type).GenerateInstance(generator)),
        genericTypes);
    }

    //should this be in this dll? or maybe inline generators should be here?

    public IResolution<T> CreateResolutionOfArray()
    {
      return new ResolutionOfArrays<T>(); //todo
    }
  }
}