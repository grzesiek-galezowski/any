namespace TddXt.AnyExtensibility
{
  public interface BasicGenerator
  {
    T Instance<T>();
    T Instance<T>(params GenerationCustomization[] customizations);
    T InstanceOf<T>(InlineGenerator<T> gen);
  }
}