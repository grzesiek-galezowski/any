using System;
using System.Diagnostics;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution;

public class FillFieldsCustomization : GeneratedObjectCustomization
{
  public void ApplyTo(object generatedObject, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var smartType = SmartType.For(generatedObject.GetType());
    var fields = smartType.GetAllPublicInstanceFields();
    foreach (var field in fields.Where(f => !request.ReachedRecursionLimit(f.FieldType)))
    {
      try
      {
        if (field.IsNullOrDefault(generatedObject))
        {
          field.SetValue(generatedObject, instanceGenerator.Instance(field.FieldType, request));
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
