using System.Reflection;
using TddEbook.TddToolkit.TypeResolution.Interfaces;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.TypeResolution.FakeChainElements
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

    public T Apply(InstanceGenerator instanceGenerator)
    {
      return _valueGenerator.Value<T>();
    }
  }
}