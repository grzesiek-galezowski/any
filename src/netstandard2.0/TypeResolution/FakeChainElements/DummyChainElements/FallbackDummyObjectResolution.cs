using System;
using System.Linq;
using System.Runtime.Serialization;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;

namespace TddXt.TypeResolution.FakeChainElements.DummyChainElements;

public class FallbackDummyObjectResolution : IResolution
{
  public object? Apply(InstanceGenerator allGenerator, GenerationRequest generationRequest, Type type)
  {
    object result;
    try
    {
      result = SmartType.For(type).GetPublicParameterlessConstructor().Value().Invoke(Enumerable.Empty<object>());
    }
    catch (Exception e)
    {
      result = FormatterServices.GetUninitializedObject(type);
      
    }
    generationRequest.CustomizeCreatedValue(result, allGenerator);
    return result;
  }

  public bool AppliesTo(Type type)
  {
    return true;
  }
}
