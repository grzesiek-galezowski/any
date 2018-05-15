using System;
using TddXt.AnyExtensibility;

namespace TddEbook.TypeReflection
{
  public interface FactoryForInstancesOfGenericTypes
  {
    object NewInstanceOf(Type type, InstanceGenerator instanceGenerator);
  }
}
