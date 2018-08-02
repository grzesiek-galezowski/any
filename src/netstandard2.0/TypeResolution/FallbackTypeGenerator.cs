using System;
using System.Collections.Generic;
using FluentAssertions;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution
{
  public class FallbackTypeGenerator<T>
  {
    private readonly FallbackTypeGenerator _fallbackTypeGenerator;

    public FallbackTypeGenerator()
    {
      var type = typeof (T);
      _fallbackTypeGenerator = new FallbackTypeGenerator(type);
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var generateInstance = (T)_fallbackTypeGenerator.GenerateInstance(instanceGenerator);
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(generateInstance, instanceGenerator);
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
      _fallbackTypeGenerator.FillFieldsAndPropertiesOf(result, instanceGenerator);
    }
  }

  public class FallbackTypeGenerator
  {
    private readonly IType _smartType;
    private readonly Type _type;

    public FallbackTypeGenerator(Type type)
    {
      _smartType = SmartType.For(type);
      _type = type;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var instance = _smartType.PickConstructorWithLeastNonPointersParameters().Value().InvokeWithParametersCreatedBy(instanceGenerator.Instance);
      instance.GetType().Should().Be(_type);
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


    public void FillFieldsAndPropertiesOf(object result, InstanceGenerator instanceGenerator)
    {
      FillPropertyValues(result, instanceGenerator);
      FillFieldValues(result, instanceGenerator);
    }

    private void FillFieldValues(object result, InstanceGenerator instanceGenerator)
    {
      var fields = _smartType.GetAllPublicInstanceFields();
      foreach (var field in fields)
      {
        try
        {
          field.SetValue(result, instanceGenerator.Instance(field.FieldType));
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }

    private void FillPropertyValues(object result, InstanceGenerator instanceGenerator)
    {
      var properties = _smartType.GetPublicInstanceWritableProperties();

      foreach (var property in properties)
      {
        try
        {
          var propertyType = property.PropertyType;

          if (!property.HasAbstractGetter())
          {
            var value = instanceGenerator.Instance(propertyType);
            property.SetValue(result, value);
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }
  }


}