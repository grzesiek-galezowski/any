using System;

namespace TddXt.AnyExtensibility
{
  public interface InstanceGenerator : BasicGenerator
  {
    object Instance(Type type);
    T ValueOtherThan<T>(params T[] omittedValues);
    T Value<T>();
    T Value<T>(T seed);
    T OtherThan<T>(params T[] omittedValues);
    T Dummy<T>();
  }

}