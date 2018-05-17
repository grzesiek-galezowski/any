using System;
using TddXt.AnyExtensibility;

namespace Generators.ImplementationDetails
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type, InstanceGenerator instanceGenerator);
  }
}
