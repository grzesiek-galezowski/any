using System;
using System.Collections.Generic;
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

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return (T)_fallbackTypeGenerator.GenerateCustomizedInstance(instanceGenerator, request);
    }

    public List<object> GenerateConstructorParameters(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return _fallbackTypeGenerator.GenerateConstructorParameters(instanceGenerator.Instance, request);
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      return _fallbackTypeGenerator.ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType();
    }


    public void FillFieldsAndPropertiesOf(object result, InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      _fallbackTypeGenerator.CustomizeCreatedValue(result, instanceGenerator, request);
    }
  }

  public class FallbackTypeGenerator
  {
    private readonly IFallbackGeneratedObjectCustomization[] _customizations;
    private readonly IType _smartType;

    public FallbackTypeGenerator(IFallbackGeneratedObjectCustomization[] customizations, ISmartType smartType)
    {
      _smartType = smartType;
      _customizations = customizations;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var maybeConstructor = _smartType.PickConstructorWithLeastNonPointersParameters();

      if (maybeConstructor.HasValue)
      {
        maybeConstructor.Value.DumpInto(request);
        var instance = maybeConstructor.Value
          .InvokeWithParametersCreatedBy(instanceGenerator.Instance, request);
        _smartType.AssertMatchesTypeOf(instance);
        return instance;
      }
      else
      {
        throw new ConstructorNotFoundException(_smartType.ToString());
      }
    }

    public List<object> GenerateConstructorParameters(Func<Type, GenerationRequest, object> parameterFactory,
      GenerationRequest request)
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      if (constructor.HasValue)
      {
        return constructor.Value
          .GenerateAnyParameterValues(parameterFactory, request);
      }
      else
      {
        throw new ConstructorNotFoundException(_smartType.ToString());
      }
    }

    public bool ConstructorIsInternalOrHasAtLeastOneNonConcreteArgumentType()
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      if (constructor.HasValue)
      {
        return constructor.Value
                 .HasAbstractOrInterfaceArguments()
               || constructor.Value.IsInternal();
      }
      else
      {
        throw new ConstructorNotFoundException(_smartType.ToString());
      }
    }

    public void CustomizeCreatedValue(object result, InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      foreach (var customization in _customizations)
      {
        customization.ApplyTo(
          _smartType, 
          result, 
          instanceGenerator, request);
      }
    }

    public object GenerateCustomizedInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var generateInstance = GenerateInstance(instanceGenerator, request);
      CustomizeCreatedValue(generateInstance, instanceGenerator, request);
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
}
