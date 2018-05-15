using System;

namespace TddXt.AnyExtensibility
{
  public interface BasicGenerator
  {
    T Instance<T>();
    T InstanceOf<T>(InlineGenerator<T> gen);
  }

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