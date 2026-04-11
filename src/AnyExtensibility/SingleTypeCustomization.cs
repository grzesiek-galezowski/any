using System;

namespace TddXt.AnyExtensibility;

public interface GenerationCustomization
{
  bool AppliesTo(Type type);
  object Generate(Type type, InstanceGenerator gen, GenerationRequest request);
}

public class SingleTypeCustomization<T>(Func<InstanceGenerator, GenerationRequest, T> func) : GenerationCustomization
{
  public bool AppliesTo(Type type)
  {
    return type == typeof(T);
  }

  public object Generate(Type type, InstanceGenerator gen, GenerationRequest request)
  {
    return func(gen, request)!;
  }
}
