using System;
using System.Linq;
using AutoFixture;
using Generators;
using TddEbook.TddToolkit.TypeResolution.Interfaces;

namespace TddEbook.TddToolkit.Generators
{
  [Serializable]
  public class ValueGenerator : IValueGenerator
  {
    private readonly Fixture _generator;

    public ValueGenerator(Fixture generator)
    {
      _generator = generator;
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      omittedValues = omittedValues ?? Array.Empty<T>();
      T currentValue;
      do
      {
        currentValue = Value<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T Value<T>()
    {
      //todo get back to it later
      return new ValueGenerator2<T>(_generator).GenerateInstance();
    }

    public T Value<T>(T seed)
    {
      return _generator.Create(seed);
    }
  }
}