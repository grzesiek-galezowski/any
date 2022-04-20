using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution;

public class FillPropertiesCustomization : IFallbackGeneratedObjectCustomization
{
  public void ApplyTo(IType smartType, object result, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var properties = smartType.GetPublicInstanceWritableProperties();

    foreach (var property in properties)
    {
      try
      {
        var propertyType = property.PropertyType;

        if (!property.HasAbstractGetter())
        {
          var value = instanceGenerator.Instance(propertyType, request);
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