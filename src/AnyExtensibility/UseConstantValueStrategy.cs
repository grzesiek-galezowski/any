namespace TddXt.AnyExtensibility;

public class UseConstantValueStrategy(int length) : ManyStrategy
{
  public int GetMany(GenerationRequest generationRequest)
  {
    return length;
  }
}
