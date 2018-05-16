using System.Threading.Tasks;
using TddXt.AnyExtensibility;

namespace Generators
{
  public class StartedTaskGenerator<T> : InlineGenerator<Task<T>>
  {
    public Task<T> GenerateInstance(InstanceGenerator genericGenerator)
    {
      return Task.FromResult(genericGenerator.Instance<T>());
    }
  }
}