using System.Collections.Generic;
using System.Linq;
using Functional.Maybe;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Numbers
{
  public class IntegerFromSequenceGenerator : InlineGenerator<int>
  {
    private static readonly HashSet<IntegerSequence> Sequences = new HashSet<IntegerSequence>();
    private readonly InlineGenerator<int> _simpleValueGenerator;
    private readonly int _startingValue;
    private readonly int _step;

    public IntegerFromSequenceGenerator(int startingValue, int step, InlineGenerator<int> intGenerator)
    {
      _startingValue = startingValue;
      _step = step;
      _simpleValueGenerator = intGenerator;
    }

    public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
    {
      var sequence = new IntegerSequence(_startingValue, _step, _simpleValueGenerator.GenerateInstance(instanceGenerator, request));
      var finalSequence = Sequences.FirstOrDefault(s => s.Equals(sequence))
        .ToMaybe()
        .OrElse(sequence);
      Sequences.Add(finalSequence);
      return finalSequence.Next();
    }
  }
}