﻿using System;
using System.Linq;
using Core.Maybe;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic.ImplementationDetails;

public class CustomizedGenerator : InstanceGenerator
{
  private readonly CustomizableInstanceGenerator _inner;

  public CustomizedGenerator(CustomizableInstanceGenerator inner)
  {
    _inner = inner;
  }

  public T OtherThan<T>(params T[] omittedValues)
  {
    return _inner.OtherThan(omittedValues);
  }

  public object OtherThan(Type type, object[] skippedValues, GenerationRequest request)
  {
    return _inner.OtherThan(type, skippedValues, request);
  }

  public object Instance(Type type, GenerationRequest request)
  {
    return request.GenerationCustomizations.Where(c => c.AppliesTo(type)).FirstMaybe()
      .SelectOrElse(
        c => c.Generate(type, this, request),
        () => _inner.Instance(type, request, request.GenerationCustomizations));
  }

  public T Dummy<T>(GenerationRequest request)
  {
    return _inner.Dummy<T>(request);
  }

  public T Instance<T>(GenerationRequest request)
  {
    return _inner.Instance<T>(request, request.GenerationCustomizations);
  }

  public object Dummy(GenerationRequest request, Type type)
  {
    throw new NotImplementedException();
  }
}
