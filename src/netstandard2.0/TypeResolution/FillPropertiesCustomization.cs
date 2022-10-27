using System;
using System.Diagnostics;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution;

public class FillPropertiesCustomization : GeneratedObjectCustomization
{
  public void ApplyTo(object generatedObject, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var smartType = SmartType.For(generatedObject.GetType());
    var properties = smartType.GetPublicInstanceWritableProperties();

    foreach (var property in properties)
    {
      try
      {
        var propertyType = property.PropertyType;

        if (!property.HasAbstractGetter() && property.HasPublicSetter())
        {
          var value = instanceGenerator.Instance(propertyType, request);
          property.SetValue(generatedObject, value);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        if (Debugger.IsAttached)
        {
          Console.WriteLine(e);
        }
      }
    }
  }
}
