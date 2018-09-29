using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Types;
using TddXt.AnyExtensibility;
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

    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var generateInstance = (T)_fallbackTypeGenerator.GenerateInstance(instanceGenerator);
      _fallbackTypeGenerator.CustomizeCreatedValue(generateInstance, instanceGenerator);
      return generateInstance;
    }

    public List<object> GenerateConstructorParameters(InstanceGenerator instanceGenerator)
    {
      return _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator.Instance);
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }


    public void FillFieldsAndPropertiesOf(T result, InstanceGenerator instanceGenerator)
    {
      _fallbackTypeGenerator.CustomizeCreatedValue(result, instanceGenerator);
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

    public object GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var instance = _smartType.PickConstructorWithLeastNonPointersParameters().Value()
        .InvokeWithParametersCreatedBy(instanceGenerator.Instance);
      _smartType.AssertMatchesTypeOf(instance);
      return instance;
    }

    public List<object> GenerateConstructorParameters(Func<Type, object> parameterFactory)
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      var constructorParameters = constructor.Value()  //bug backward compatibility (for now)
        .GenerateAnyParameterValues(parameterFactory);
      return constructorParameters;
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      return constructor.Value() //bug backward compatibility (for now)
        .HasAbstractOrInterfaceArguments()
      || constructor.Value().IsInternal();
    }


    public void CustomizeCreatedValue(object result, InstanceGenerator instanceGenerator)
    {
      foreach (var customization in _customizations)
      {
        customization.ApplyTo(
          _smartType, 
          result, 
          instanceGenerator);
      }
    }
  }


}