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
    internal AllGenerator AllGenerator;

    public MyGenerator(AllGenerator allGenerator)
    {
      this.AllGenerator = allGenerator;
    }
  }
}