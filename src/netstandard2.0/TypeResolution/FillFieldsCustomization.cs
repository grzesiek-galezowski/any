using System;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution
{
  public class FillFieldsCustomization : IFallbackGeneratedObjectCustomization
  {
    public void ApplyTo(IType smartType, object result, InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      var fields = smartType.GetAllPublicInstanceFields();
      foreach (var field in fields)
      {
        try
        {
          if (field.IsNullOrDefault(result))
          {
            field.SetValue(result, instanceGenerator.Instance(field.FieldType, trace));
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