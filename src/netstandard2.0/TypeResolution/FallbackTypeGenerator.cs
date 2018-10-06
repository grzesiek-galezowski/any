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
    private readonly IFallbackGeneratedObjectCustomization[] _customizations;
    private readonly IType _smartType;

    public FallbackTypeGenerator(IFallbackGeneratedObjectCustomization[] customizations, ISmartType smartType)
    {
      _smartType = smartType;
      _customizations = customizations;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var maybeConstructor = _smartType.PickConstructorWithLeastNonPointersParameters();

      if (maybeConstructor.HasValue)
      {
        maybeConstructor.Value().DumpInto(trace);
        var instance = maybeConstructor.Value()
          .InvokeWithParametersCreatedBy(instanceGenerator.Instance, trace);
        _smartType.AssertMatchesTypeOf(instance);
        return instance;
      }
      else
      {
        throw new ConstructorNotFoundException(_smartType.ToString());
      }
    }

    public List<object> GenerateConstructorParameters(Func<Type, GenerationTrace, object> parameterFactory,
      GenerationTrace trace)
    {
      var constructor = _smartType.PickConstructorWithLeastNonPointersParameters();
      if (constructor.HasValue)
      {
        var constructorParameters = constructor.Value()
          .GenerateAnyParameterValues(parameterFactory, trace);
        return constructorParameters;
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
        return constructor.Value()
                 .HasAbstractOrInterfaceArguments()
               || constructor.Value().IsInternal();
      }
      else
      {
        throw new ConstructorNotFoundException(_smartType.ToString());
      }
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

  public class ConstructorNotFoundException : Exception
  {
    public ConstructorNotFoundException(string typeDescription)
    : base(typeDescription)
    {
      
    }
  }
}