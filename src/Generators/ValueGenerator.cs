using System.Linq;
using AutoFixture;
using TddEbook.TddToolkit.TypeResolution.Interfaces;

namespace TddEbook.TddToolkit.Generators
{
  public class ValueGenerator : IValueGenerator
  {
    private readonly Fixture _generator;

    public ValueGenerator(Fixture generator)
    {
      _generator = generator;
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      T currentValue;
      do
      {
        currentValue = ValueOf<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T ValueOf<T>()
    {
      //todo get back to it later
      return new ValueGenerator2<T>(_generator).GenerateInstance();
    }

    public T ValueOf<T>(T seed)
    {
      return _generator.Create(seed);
    }
  }
}