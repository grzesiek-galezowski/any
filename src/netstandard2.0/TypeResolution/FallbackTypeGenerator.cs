using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution;

public class FallbackTypeGenerator
{
  private readonly IFallbackGeneratedObjectCustomization[] _customizations;

  public FallbackTypeGenerator(IFallbackGeneratedObjectCustomization[] customizations)
  {
    _customizations = customizations;
  }

  public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var smartType = SmartType.For(type);
    var maybeConstructor = smartType.PickConstructorWithLeastNonPointersParameters();

    if (maybeConstructor.HasValue)
    {
      maybeConstructor.Value().DumpInto(request);
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

  public void CustomizeCreatedValue(
    object result, 
    InstanceGenerator instanceGenerator, 
    GenerationRequest request,
    Type type)
  {
    var smartType = SmartType.For(type);
    foreach (var customization in _customizations)
    {
      customization.ApplyTo(
        smartType, 
        result, 
        instanceGenerator, request);
    }
  }

  public object GenerateCustomizedInstance(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var generateInstance = GenerateInstance(instanceGenerator, request, type);
    CustomizeCreatedValue(generateInstance, instanceGenerator, request, type);
    return generateInstance;
  }
}

public class ConstructorNotFoundException : Exception
{
  public ConstructorNotFoundException(string typeDescription)
    : base(typeDescription)
  {
      
  }
}