namespace TddXt.AnyExtensibility
{
  public interface InlineGenerator<out T>
  {
    T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace);
  }

}