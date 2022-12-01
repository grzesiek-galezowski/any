using System;

namespace TddXt.AnyExtensibility;

public interface NestingLimit
{
  void AddNestingFor(Type type, GenerationTrace generationTrace);
  bool IsReachedFor(Type type);
  void RemoveNestingFor(Type type, GenerationTrace generationTrace);
}
