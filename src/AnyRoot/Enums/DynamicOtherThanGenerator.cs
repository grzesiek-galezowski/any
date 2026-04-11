using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyRoot.Enums;

public class DynamicOtherThanGenerator(Type type, IEnumerable<object> excludedValues) : InlineGenerator<object>
{
  public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return instanceGenerator.OtherThan(type, excludedValues.ToArray(), request);
  }
}
