using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution;

public interface IInterceptedInvocation
{
  bool HasReturnValue();
  bool IsPropertySetter();
  bool IsPropertyGetter();
  void GenerateAndAddPropertyGetterReturnValueTo(PerMethodCache<object> perMethodCache);
  void GenerateAndAddMethodReturnValueTo(PerMethodCache<object> perMethodCache, GenerationRequest request);
}
