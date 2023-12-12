using System.Collections.Generic;
using System.Linq;
using Core.Maybe;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Numbers;

public class IntegerFromSequenceGenerator(int startingValue, int step, InlineGenerator<int> intGenerator)
  : InlineGenerator<int>
{
  private static readonly HashSet<IntegerSequence> Sequences = new();

  public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    var sequence = new IntegerSequence(startingValue, step, intGenerator.GenerateInstance(instanceGenerator, request));
    var finalSequence = Sequences.FirstOrDefault(s => s.Equals(sequence))
      .ToMaybe()
      .OrElse(sequence);
    Sequences.Add(finalSequence);
    return finalSequence.Next();
  }
}
