using TddXt.AnyExtensibility;
using TddXt.TypeReflection.Interfaces;

namespace TddXt.TypeResolution
{
  public interface IFallbackGeneratedObjectCustomization
  {
    void ApplyTo(IType smartType, object result, InstanceGenerator instanceGenerator, GenerationTrace trace);
  }
}