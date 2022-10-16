using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution;

public interface FactoryForInstancesOfGenericTypes
{
  object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationRequest request);
}