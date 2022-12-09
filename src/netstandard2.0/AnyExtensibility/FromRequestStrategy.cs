namespace TddXt.AnyExtensibility;

public class FromRequestStrategy : ManyStrategy
{
  public int GetMany(GenerationRequest generationRequest)
  {
    return generationRequest.Many;
  }
}