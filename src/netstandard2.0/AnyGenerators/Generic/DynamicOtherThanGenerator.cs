using System;
using System.Collections.Generic;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Generic
{
  public class DynamicOtherThanGenerator : InlineGenerator<object>
  {
    private readonly Type _type;
    private readonly IEnumerable<object> _excludedValues;

    public DynamicOtherThanGenerator(Type type, IEnumerable<object> excludedValues)
    {
      _type = type;
      _excludedValues = excludedValues;
    }

    public object GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      return instanceGenerator.OtherThan(_type, _excludedValues.ToArray(), request);
    }
  }
}