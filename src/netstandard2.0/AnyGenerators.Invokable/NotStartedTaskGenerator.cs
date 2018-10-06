using System.Threading.Tasks;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Invokable
{
  public class NotStartedTaskGenerator : InlineGenerator<Task>
  {
    public Task GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return new Task(() => Task.Delay(1).Wait());
    }
  }

  public class NotStartedTaskGenerator<T> : InlineGenerator<Task<T>>
  {
    public Task<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return new Task<T>(() => instanceGenerator.Instance<T>(trace));
    }
  }
}