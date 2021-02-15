using System;

namespace TddXt.AnyExtensibility
{
  public interface InstanceGenerator
  {
    T ValueOtherThan<T>(params T[] omittedValues);
    T Value<T>(GenerationRequest request);
    T Value<T>(T seed, GenerationRequest request);
    T OtherThan<T>(params T[] omittedValues);
    object OtherThan(Type type, object[] omittedValues, GenerationRequest request);
    object Instance(Type type, GenerationRequest request);
    T Dummy<T>(GenerationRequest request);
    T Instance<T>(GenerationRequest request);
  }

  public interface CustomizableInstanceGenerator : InstanceGenerator
  {
    object Instance(Type type, GenerationRequest request, GenerationCustomization[] customizations);
    T Instance<T>(GenerationRequest request, GenerationCustomization[] customizations);
    T Value<T>(GenerationRequest request, GenerationCustomization[] customizations);
  }

}