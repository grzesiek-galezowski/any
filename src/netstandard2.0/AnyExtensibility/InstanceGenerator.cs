using System;

namespace TddXt.AnyExtensibility
{
  public interface InstanceGenerator
  {
    T ValueOtherThan<T>(params T[] omittedValues);
    T Value<T>(GenerationTrace trace);
    T Value<T>(T seed, GenerationTrace trace);
    T OtherThan<T>(params T[] omittedValues);
    object Instance(Type type, GenerationTrace trace);
    T Dummy<T>(GenerationTrace trace);
    T Instance<T>(GenerationTrace trace);
  }

}