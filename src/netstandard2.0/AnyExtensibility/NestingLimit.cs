namespace TddXt.AnyExtensibility;

public interface NestingLimit
{
  void AddNestingFor<T>(GenerationTrace generationTrace);
  bool IsReachedFor<T>();
  void RemoveNestingFor<T>(GenerationTrace generationTrace);
}
