using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.Interfaces;

public interface IValueGenerator
{
  T Value<T>(InstanceGenerator gen, GenerationRequest request);
  object Value(Type type, InstanceGenerator gen, GenerationRequest request);
}