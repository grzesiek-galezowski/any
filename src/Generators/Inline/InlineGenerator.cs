using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public interface InlineGenerator<out T>
  {
    T GenerateInstance(IInstanceGenerator instanceGenerator);
  }

}