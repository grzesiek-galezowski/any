using TddEbook.TddToolkit.Generators;
using TddEbook.TddToolkit.Subgenerators;

namespace AnyCore
{
  public class Core
  {
    public static MyGenerator Any { get; } = new MyGenerator(AllGeneratorFactory.Create()); //todo refactor
  }

  public class MyGenerator
  {
    internal readonly AllGenerator AllGenerator;

    public MyGenerator(AllGenerator allGenerator)
    {
      this.AllGenerator = allGenerator;
    }

    public T InstanceOf<T>(InlineGenerator<T> gen)
    {
      return gen.GenerateInstance(this.AllGenerator);
    }
  }
}