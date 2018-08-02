namespace TddXt.AnyExtensibility
{
  public interface BasicGenerator
  {
    T Instance<T>();
    T InstanceOf<T>(InlineGenerator<T> gen);
  }
}