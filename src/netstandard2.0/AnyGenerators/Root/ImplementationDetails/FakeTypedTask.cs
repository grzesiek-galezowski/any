using System.Linq;
using System.Threading.Tasks;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Generic.ImplementationDetails;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public class FakeTypedTask<T> : IResolution<T>
  {
    public bool Applies()
    {
      return typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Task<>);
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var resultType = typeof(T).GenericTypeArguments.First();
      var parameters = instanceGenerator.Instance(resultType, request);
      var result = new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<Task>(
        typeof(T).GenericTypeArguments.First(),
        "FromResult", parameters);
      return (T) result;
    }
  }
}