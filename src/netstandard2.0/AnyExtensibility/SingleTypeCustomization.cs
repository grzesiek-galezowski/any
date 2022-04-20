using System;

namespace TddXt.AnyExtensibility;

public interface GenerationCustomization
{
  bool AppliesTo(Type type);
  object Generate(Type type, InstanceGenerator gen, GenerationRequest request);
}

public class SingleTypeCustomization<T> : GenerationCustomization
{
  private readonly Func<InstanceGenerator, GenerationRequest, T> _func;

  public SingleTypeCustomization(Func<InstanceGenerator, GenerationRequest, T> func)
  {
    _func = func;
  }

  public bool AppliesTo(Type type)
  {
    return type == typeof(T);
  }

  public object Generate(Type type, InstanceGenerator gen, GenerationRequest request)
  {
    return _func(gen, request)!;
  }
}