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

    public ValueGenerator(FixtureWrapper fixtureWrapper)
    {
      _generator = fixtureWrapper;
    }

    public T Value<T>(InstanceGenerator gen, GenerationRequest request)
    {
      return WithCustomizations(gen, request, fixture => fixture.Create<T>());
    }

    public T ValueOtherThan<T>(
      InstanceGenerator gen, 
      GenerationRequest request,
      T[]? omittedValues)
    {
      omittedValues ??= new T[] { };
      T currentValue;
      do
      {
        currentValue = Value<T>(gen, request);
      } while (omittedValues.Contains(currentValue));

      return currentValue;
    }

    public T Value<T>(InstanceGenerator gen, GenerationRequest request, T seed)
    {
      return WithCustomizations(gen, request, wrapper => wrapper.Create(seed));
    }

    private T WithCustomizations<T>(
      InstanceGenerator gen, 
      GenerationRequest request, 
      Func<FixtureWrapper, T> creation)
    {
      using (_generator.Customize(request, gen))
      {
        return creation.Invoke(_generator);
      }
    }
  }
}
