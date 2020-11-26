using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Math
{
  public class IntegerDivisibleByGenerator : InlineGenerator<int>
  {
    private static readonly CircularList<int> NumbersToMultiply 
      = CircularList.CreateStartingFromRandom(Enumerable.Range(1, 100).ToArray());

    private readonly int _quotient;

    public IntegerDivisibleByGenerator(int quotient)
    {
      _quotient = quotient;
    }

    public int GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return NumbersToMultiply.Next() * _quotient;
    }
  }
}