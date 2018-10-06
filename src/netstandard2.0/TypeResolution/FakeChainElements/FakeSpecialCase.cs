using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class FakeSpecialCase<T> : IResolution<T>
  {
    private readonly IValueGenerator _valueGenerator;

    public FakeSpecialCase(IValueGenerator valueGenerator)
    {
      _valueGenerator = valueGenerator;
    }

    public bool Applies()
    {
      return 
        TypeOfType.Is<T>() || 
        typeof(T) == typeof(MethodInfo);
    }

    public T Apply(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return _valueGenerator.Value<T>();
    }
  }
}