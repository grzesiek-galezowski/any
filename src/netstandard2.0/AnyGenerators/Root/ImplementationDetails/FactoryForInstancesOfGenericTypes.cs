using System;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Root.ImplementationDetails
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type, InstanceGenerator instanceGenerator, GenerationTrace trace);
  }
}
