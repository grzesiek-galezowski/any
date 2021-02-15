using System.Threading.Tasks;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Invokable
{
  public class StartedTaskGenerator<T> : InlineGenerator<Task<T>>
  {
    public Task<T> GenerateInstance(InstanceGenerator genericGenerator, GenerationRequest request)
    {
      return Task.FromResult(genericGenerator.Instance<T>(request));
    }
  }

  public class StartedTaskGenerator : InlineGenerator<Task>
  {
    public Task GenerateInstance(InstanceGenerator genericGenerator, GenerationRequest request)
    {
      return Task.Factory.StartNew(() => { });
    }
  }
}