using System;
using System.Linq;
using Functional.Maybe;
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

    public T Value<T>(GenerationRequest request)
    {
      return _inner.Value<T>(request, _customizations);
    }

    public T Value<T>(T seed, GenerationRequest request)
    {
      return _inner.Value(seed, request);
    }

    public T OtherThan<T>(params T[] omittedValues)
    {
      return _inner.OtherThan(omittedValues);
    }

    public object OtherThan(Type type, object[] omittedValues, GenerationRequest request)
    {
      return _inner.OtherThan(type, omittedValues, request);
    }

    public object Instance(Type type, GenerationRequest request)
    {
      return _customizations.Where(c => c.AppliesTo(type)).FirstMaybe()
        .SelectOrElse(
          c => c.Generate(type, this, request),
          () => _inner.Instance(type, request, _customizations));
    }

    public T Dummy<T>(GenerationRequest request)
    {
      return _inner.Dummy<T>(request);
    }

    public T Instance<T>(GenerationRequest request)
    {
      return _inner.Instance<T>(request, _customizations);
    }
  }
}