using System;
using System.Linq;
using TddXt.AutoFixtureWrapper;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class ValueGenerator : IValueGenerator
  {
    private readonly FixtureWrapper _generator;

    public ValueGenerator(FixtureWrapper fixtureWrapper)
    {
      _generator = fixtureWrapper;
    }

    public T Value<T>()
    {
      return _generator.Create<T>();
    }

    public T ValueOtherThan<T>(params T[] omittedValues)
    {
      omittedValues = omittedValues ?? new T[] { };
      T currentValue;
      do
      {
        currentValue = Value<T>();
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T Value<T>(T seed)
    {
      return _generator.Create(seed);
    }
  }
}