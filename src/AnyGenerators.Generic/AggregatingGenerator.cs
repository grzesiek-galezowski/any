using System;
using System.Linq;
using TddXt.AnyExtensibility;

namespace Generators
{

  public class AggregatingGenerator<T> : InlineGenerator<T>
  {
    private readonly InlineGenerator<T>[] _generators;
    private readonly T _identity;
    private readonly Func<T, T, T> _addOperation;

    public AggregatingGenerator(T identity, Func<T, T, T> addOperation,
      params InlineGenerator<T>[] inlineGenerators)
    {
      _generators = inlineGenerators;
      _identity = identity;
      _addOperation = addOperation;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return _generators.Aggregate(_identity, 
        (current, generator) => _addOperation(current, generator.GenerateInstance(instanceGenerator)));
    }
  }
}