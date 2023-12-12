using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution;

public class ObjectGenerator
{
  private object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var smartType = SmartType.For(type);
    var maybeConstructor = smartType.PickConstructorWithLeastNonPointersParameters();

    if (maybeConstructor.HasValue)
    {
      maybeConstructor.Value().LogInScopeOf(request);
      var instance = maybeConstructor.Value()
        .InvokeWithParametersCreatedBy(instanceGenerator.Instance, request);
      smartType.AssertMatchesTypeOf(instance);
      return instance;
    }
    else
    {
      throw new ConstructorNotFoundException(smartType.ToString());
    }
  }

  public List<object> GenerateConstructorParameters(Func<Type, GenerationRequest, object> parameterFactory,
    GenerationRequest request, Type type)
  {
    var smartType = SmartType.For(type);
    var constructor = smartType.PickConstructorWithLeastNonPointersParameters();
    if (constructor.HasValue)
    {
      return constructor.Value()
        .GenerateAnyParameterValues(parameterFactory, request);
    }
    else
    {
      throw new ConstructorNotFoundException(smartType.ToString());
    }
  }

  public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType(Type type)
  {
    var smartType = SmartType.For(type);
    var constructor = smartType.PickConstructorWithLeastNonPointersParameters();
    if (constructor.HasValue)
    {
      return constructor.Value()
               .HasAbstractOrInterfaceArguments()
             || constructor.Value().IsInternal();
    }
    else
    {
      throw new ConstructorNotFoundException(smartType.ToString());
    }
  }

  public object GenerateCustomizedInstance(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var generateInstance = GenerateInstance(instanceGenerator, request, type);
    request.CustomizeCreatedValue(generateInstance, instanceGenerator);
    return generateInstance;
  }
}

public class ConstructorNotFoundException(string typeDescription) : Exception(typeDescription);
