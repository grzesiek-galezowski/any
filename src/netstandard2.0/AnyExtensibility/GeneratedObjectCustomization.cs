namespace TddXt.AnyExtensibility;

public interface GeneratedObjectCustomization
{
  void ApplyTo(
    object generatedObject,
    InstanceGenerator instanceGenerator,
    GenerationRequest request);
}
