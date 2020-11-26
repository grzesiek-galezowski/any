using System;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails
{
  public class CustomizedGenerator : InstanceGenerator
  {
    private readonly GenerationCustomization[] _customizations;
    private readonly CustomizableInstanceGenerator _inner;

    public CustomizedGenerator(CustomizableInstanceGenerator inner, GenerationCustomization[] customizations)
    {
      _inner = inner;
      _customizations = customizations;
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      return _inner.ValueOtherThan(omittedValues);
    }

    public T Value<T>(GenerationTrace trace)
    {
      return _inner.Value<T>(trace, _customizations);
    }

    public T Value<T>(T seed, GenerationTrace trace)
    {
      return _inner.Value(seed, trace);
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _inner.OtherThan(omittedValues);
    }

    public object Instance(Type type, GenerationTrace trace)
    {
      return _customizations.Where(c => c.AppliesTo(type)).FirstOrNothing()
        .Fold(
          () => _inner.Instance(type, trace, _customizations), 
          c => c.Generate(type, this, trace));
    }

    public T Dummy<T>(GenerationTrace trace)
    {
      return _inner.Dummy<T>(trace);
    }

    public T Instance<T>(GenerationTrace trace)
    {
      return _inner.Instance<T>(trace, _customizations);
    }
  }
}