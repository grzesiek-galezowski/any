﻿using System;

namespace TddXt.AnyExtensibility
{
  public interface InstanceGenerator
  {
    T ValueOtherThan<T>(params T[] omittedValues);
    T Value<T>(GenerationTrace trace);
    T Value<T>(T seed, GenerationTrace trace);
    T OtherThan<T>(params T[] omittedValues);
    object OtherThan(Type type, object[] omittedValues, GenerationTrace trace);
    object Instance(Type type, GenerationTrace trace);
    T Dummy<T>(GenerationTrace trace);
    T Instance<T>(GenerationTrace trace);
  }

  public interface CustomizableInstanceGenerator : InstanceGenerator
  {
    object Instance(Type type, GenerationTrace trace, GenerationCustomization[] customizations);
    T Instance<T>(GenerationTrace trace, GenerationCustomization[] customizations);
    T Value<T>(GenerationTrace trace, GenerationCustomization[] customizations);
  }

}