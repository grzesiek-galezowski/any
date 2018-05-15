using System.Threading.Tasks;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

public class NotStartedTaskGenerator : InlineGenerator<Task>
{
  public Task GenerateInstance(InstanceGenerator instanceGenerator)
  {
    return new Task(() => Task.Delay(1).Wait());
  }
}

public class NotStartedTaskGenerator<T> : InlineGenerator<Task<T>>
{
  public Task<T> GenerateInstance(InstanceGenerator instanceGenerator)
  {
    return new Task<T>(instanceGenerator.Instance<T>);
  }
}