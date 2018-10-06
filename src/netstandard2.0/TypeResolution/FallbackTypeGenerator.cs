using System;
using System.Collections.Generic;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution
{
  public class FallbackTypeGenerator<T>
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;

    public FallbackTypeGenerator(FallbackTypeGenerator fallbackTypeGenerator)
    {
      _fallbackTypeGenerator = fallbackTypeGenerator;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var generateInstance = (T)_fallbackTypeGenerator.GenerateInstance(instanceGenerator, trace);
      _fallbackTypeGenerator.CustomizeCreatedValue(generateInstance, instanceGenerator, trace);
      return generateInstance;
    }

    public List<object> GenerateConstructorParameters(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator.Instance, trace);
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }


    public void FillFieldsAndPropertiesOf(T result, InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      _fallbackTypeGenerator.CustomizeCreatedValue(result, instanceGenerator, trace);
    }
  }

  public class FallbackTypeGenerator
  {
    private readonly IType _smartType;
    private readonly IFallbackGeneratedObjectCustomization[] _customizations;

    public FallbackTypeGenerator(IFallbackGeneratedObjectCustomization[] customizations, ISmartType smartType)
    {
      _smartType = smartType;
      _customizations = customizations;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var constructorWrapper = _smartType.PickConstructorWithLeastNonPointersParameters().Value();
      constructorWrapper.DumpInto(trace);
      var instance = constructorWrapper
        .InvokeWithParametersCreatedBy(instanceGenerator.Instance, trace);
      _smartType.AssertMatchesTypeOf(instance);
      return instance;
    }

    public List<object> GenerateConstructorParameters(Func<Type, GenerationTrace, object> parameterFactory,
      GenerationTrace trace)
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      var constructorParameters = constructor.Value()  //bug backward compatibility (for now)
        .GenerateAnyParameterValues(parameterFactory, trace);
      return constructorParameters;
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      return constructor.Value() //bug backward compatibility (for now)
        .HasAbstractOrInterfaceArguments()
      || constructor.Value().IsInternal();
    }


    public void CustomizeCreatedValue(object result, InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      foreach (var customization in _customizations)
      {
        customization.ApplyTo(
          _smartType, 
          result, 
          instanceGenerator, trace);
      }
    }
  }


}