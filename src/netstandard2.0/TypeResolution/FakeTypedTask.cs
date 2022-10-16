using System;
using System.Linq;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.TypeResolution;

public class FakeTypedTask : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var resultType = type.GenericTypeArguments.First();
    var parameters = instanceGenerator.Instance(resultType, request);
    var result = new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<Task>(
      type.GenericTypeArguments.First(),
      "FromResult", parameters);
    return result;
  }
}
