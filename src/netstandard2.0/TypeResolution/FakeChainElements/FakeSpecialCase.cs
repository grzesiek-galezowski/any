using System;
using System.Reflection;
using TddXt.AnyExtensibility;
using TddXt.TypeReflection;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.TypeResolution.FakeChainElements;

public class FakeSpecialCase : IResolution
{
  private readonly IValueGenerator _valueGenerator;

  public FakeSpecialCase(IValueGenerator valueGenerator)
  {
    _valueGenerator = valueGenerator;
  }

  public bool AppliesTo(Type type)
  {
    return 
      TypeOfType.Is(type) || 
      type == typeof(MethodInfo);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    return _valueGenerator.Value(type, instanceGenerator, request);
  }
}