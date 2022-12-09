namespace TddXt.AnyExtensibility;

public class UseConstantValueStrategy : ManyStrategy
{
  private readonly int _length;

  public UseConstantValueStrategy(int length)
  {
    _length = length;
  }

  public int GetMany(GenerationRequest generationRequest)
  {
    return _length;
  }
}
