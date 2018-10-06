using System;

namespace TddXt.AnyExtensibility
{
  public interface GenerationCustomization
  {
    bool AppliesTo(Type type);
    object Generate(InstanceGenerator gen, GenerationTrace trace);
  }

  public class SingleTypeCustomization<T> : GenerationCustomization
  {
    private readonly Func<InstanceGenerator, GenerationTrace, T> _func;

    public SingleTypeCustomization(Func<InstanceGenerator, GenerationTrace, T> func)
    {
      _func = func;
    }

    public bool AppliesTo(Type type)
    {
      return type == typeof(T);
    }

    public object Generate(InstanceGenerator gen, GenerationTrace trace)
    {
      return _func(gen, trace);
    }
  }
}