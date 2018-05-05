using System.Collections.Generic;
using System.Linq;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TddToolkit.TypeResolution;
using TddEbook.TypeReflection;

namespace TddEbook.TddToolkit.Generators
{
  public class IntegerFromSequenceGenerator : InlineGenerator<int>
  {
    private static readonly HashSet<IntegerSequence> Sequences = new HashSet<IntegerSequence>();
    private readonly int _startingValue;
    private readonly int _step;
    private readonly InlineGenerator<int> _simpleValueGenerator;

    public IntegerFromSequenceGenerator(int startingValue, int step, InlineGenerator<int> intGenerator)
    {
      _startingValue = startingValue;
      _step = step;
      _simpleValueGenerator = intGenerator;
    }

    public int GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var sequence = new IntegerSequence(_startingValue, _step, _simpleValueGenerator.GenerateInstance(instanceGenerator));
      var finalSequence = Maybe.Wrap(Sequences.FirstOrDefault(s => s.Equals(sequence))).ValueOr(sequence);
      Sequences.Add(finalSequence);
      return finalSequence.Next();
    }
  }
}