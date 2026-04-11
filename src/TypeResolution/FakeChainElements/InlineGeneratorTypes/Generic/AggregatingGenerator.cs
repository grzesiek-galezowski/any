using System;
using System.Linq;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class AggregatingGenerator<T>(
  T identity,
  Func<T, T, T> addOperation,
  params InlineGenerator<T>[] inlineGenerators)
  : InlineGenerator<T>
{
  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    return inlineGenerators.Aggregate(identity,
      (current, generator) => addOperation(current, generator.GenerateInstance(instanceGenerator, request)));
  }
}
