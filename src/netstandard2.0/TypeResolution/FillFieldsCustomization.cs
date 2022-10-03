using System;
using System.Diagnostics;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution;

public class FillFieldsCustomization : IFallbackGeneratedObjectCustomization
{
  public void ApplyTo(IType smartType, object result, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var fields = smartType.GetAllPublicInstanceFields();
    foreach (var field in fields)
    {
      try
      {
        if (field.IsNullOrDefault(result))
        {
          field.SetValue(result, instanceGenerator.Instance(field.FieldType, request));
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
