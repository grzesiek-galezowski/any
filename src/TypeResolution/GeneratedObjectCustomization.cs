using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution;

public interface GeneratedObjectCustomization
{
  void ApplyTo(
    object generatedObject,
    InstanceGenerator instanceGenerator,
    GenerationRequest request);
}
