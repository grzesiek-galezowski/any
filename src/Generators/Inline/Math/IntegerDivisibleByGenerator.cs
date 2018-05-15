using System.Linq;
using TddEbook.TddToolkit.CommonTypes;
using TddEbook.TypeReflection;
using TddXt.AnyExtensibility;

namespace TddEbook.TddToolkit.Generators
{
  public class IntegerDivisibleByGenerator : InlineGenerator<int>
  {
    private readonly int _quotient;

    private static readonly CircularList<int> NumbersToMultiply 
      = CircularList.CreateStartingFromRandom(Enumerable.Range(1, 100).ToArray());

    public IntegerDivisibleByGenerator(int quotient)
    {
      _quotient = quotient;
    }

    public int GenerateInstance(InstanceGenerator allGenerator)
    {
      return NumbersToMultiply.Next() * _quotient;
    }
  }
}