using System;

namespace TddEbook.TypeReflection
{
  public interface InstanceGenerator
  {
    T Instance<T>();
    object Instance(Type type);
    T OtherThan<T>(params T[] omittedValues);
    T Dummy<T>();
    T ValueOtherThan<T>(params T[] omittedValues);
    T Value<T>();
    T Value<T>(T seed);
  }
}