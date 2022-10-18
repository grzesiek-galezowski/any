using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution;

public interface IInterceptedInvocation
{
  bool HasReturnValue();
  bool IsPropertySetter();
  bool IsPropertyGetter();
  void GenerateAndAddPropertyGetterReturnValueTo(IPerMethodCache<object> perMethodCache);
  void GenerateAndAddMethodReturnValueTo(IPerMethodCache<object> perMethodCache, GenerationRequest request);
}
