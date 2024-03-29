﻿using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Collections;

public class NullableGenerator<T> : InlineGenerator<T?> where T : struct
{
  public T? GenerateInstance(
    InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instanceGenerator.Instance<T>(request);
  }
}
