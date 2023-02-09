using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.Maybe;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeReflection.Interfaces;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution;

public class FillPropertiesCustomization : GeneratedObjectCustomization
{
  public void ApplyTo(object generatedObject, InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var smartType = SmartType.For(generatedObject.GetType());
    FillPublicSettableProperties(generatedObject, instanceGenerator, request, smartType);
    FillPublicEmptyMutableGettableOnlyCollections(generatedObject, instanceGenerator, request, smartType);
  }

  private void FillPublicEmptyMutableGettableOnlyCollections(object generatedObject, InstanceGenerator instanceGenerator,
    GenerationRequest request, ISmartType smartType)
  {
    foreach (var property in AddablePropertiesWhereRecursionLimitIsNotYetReached(request, smartType))
    {
      try
      {
        if (MutableCollectionFiller.IsSupported(property.PropertyType))
        {
          property.GetValue(generatedObject).Do(collectionInstance =>
          {
            if (MutableCollectionFiller.IsEmpty(collectionInstance))
            {
              MutableCollectionFiller.Fill(collectionInstance, instanceGenerator, request);
            }
          });
        }
      }
      catch (Exception e)
      {
        HandleExceptionSilently(e);
      }
    }
  }

  private static void FillPublicSettableProperties(object generatedObject, InstanceGenerator instanceGenerator,
    GenerationRequest request, ISmartType smartType)
  {
    foreach (var property in SettablePropertiesWhereRecursionLimitIsNotYetReached(request, smartType))
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
        HandleExceptionSilently(e);
      }
    }
  }

  private static void HandleExceptionSilently(Exception e)
  {
    Console.WriteLine(e.Message);
    if (Debugger.IsAttached)
    {
      Console.WriteLine(e);
    }
  }

  private IEnumerable<IPropertyWrapper> AddablePropertiesWhereRecursionLimitIsNotYetReached(
    GenerationRequest request, 
    ISmartType smartType)
  {
    var publicInstanceReadableProperties = smartType.GetPublicInstanceReadableProperties();
    return publicInstanceReadableProperties.Where(p => !request.ReachedRecursionLimit(p.PropertyType));
  }

  private static IEnumerable<IPropertyWrapper> SettablePropertiesWhereRecursionLimitIsNotYetReached(GenerationRequest request, ISmartType smartType)
  {
    var settableProperties = smartType.GetPublicInstanceWritableProperties();
    return settableProperties.Where(p => !request.ReachedRecursionLimit(p.PropertyType));
  }
}
