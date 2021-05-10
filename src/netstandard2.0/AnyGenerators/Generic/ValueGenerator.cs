using System;
using System.Linq;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.AutoFixtureWrapper;
using TddXt.TypeResolution.Interfaces;

namespace TddXt.AnyGenerators.Generic
{
  [Serializable]
  public class ValueGenerator : IValueGenerator
  {
    private readonly FixtureWrapper _generator;
    private readonly object _syncRoot = new();

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
      omittedValues ??= new T[] { };
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

    public T Value<T>(InstanceGenerator gen, GenerationRequest request)
    {
      lock (_syncRoot)
      {
        using (_generator.Customize(request, gen))
        {
          return _generator.Create<T>();
        }
      }
    }
  }
}