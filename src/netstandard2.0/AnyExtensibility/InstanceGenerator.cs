﻿using System;

namespace TddXt.AnyExtensibility;

public interface InstanceGenerator
{
  T OtherThan<T>(params T[] omittedValues);
  object OtherThan(Type type, object[] skippedValues, GenerationRequest request);
  object Instance(Type type, GenerationRequest request);
  T Dummy<T>(GenerationRequest request);
  T Instance<T>(GenerationRequest request);
  object? Dummy(GenerationRequest request, Type type);
}

public interface CustomizableInstanceGenerator : InstanceGenerator
{
  object Instance(Type type, GenerationRequest request, GenerationCustomization[] customizations);
  T Instance<T>(GenerationRequest request, GenerationCustomization[] customizations);
}
