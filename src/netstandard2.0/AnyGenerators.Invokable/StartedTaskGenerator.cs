using System.Threading.Tasks;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Invokable
{
  public class StartedTaskGenerator<T> : InlineGenerator<Task<T>>
  {
    public Task<T> GenerateInstance(InstanceGenerator genericGenerator, GenerationTrace trace)
    {
      return Task.FromResult(genericGenerator.Instance<T>(trace));
    }
  }

  public class StartedTaskGenerator : InlineGenerator<Task>
  {
    public Task GenerateInstance(InstanceGenerator genericGenerator, GenerationTrace trace)
    {
      return Task.Factory.StartNew(() => { });
    }
  }
}