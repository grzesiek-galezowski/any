using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;
using TddXt.TypeResolution.ResolutionOfGenericTypes;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

public class SpecialCasesOfResolutions : ISpecialCasesOfResolutions
{
  public IResolution CreateResolutionOfKeyValuePair()
  {
    return CreateResolutionOf2GenericType(nameof(InternalInlineGenerators.KeyValuePair),
      typeof(KeyValuePair<,>)
    );
  }

  public IResolution CreateResolutionOf2GenericType(string className, params Type[] matchingTypes)
  {
    var factoryMethod = new Func<Type, Type, InstanceGenerator, GenerationRequest, object>((type1, type2, instanceGenerator, trace) =>
      InternalInlineGenerators.GetByNameAndTypes(className, type1, type2)
        .GenerateInstance(instanceGenerator, trace));

    return new ResolutionOfTypeWithGenerics(
      new FactoryForInstancesOfGenericTypesWith2Generics(
        factoryMethod),
      matchingTypes);
  }


  public IResolution CreateResolutionOf1GenericType(string resolvedTypeName, params Type[] genericTypes)
  {
    return new ResolutionOfTypeWithGenerics(
      new FactoryForInstancesOfGenericTypesWith1Generic(
        (type, generator, trace) => InternalInlineGenerators.GetByNameAndType(resolvedTypeName, type)
          .GenerateInstance(generator, trace)),
      genericTypes);
  }

  public IResolution CreateResolutionOfArray()
  {
    return new ResolutionOfArrays();
  }
}
