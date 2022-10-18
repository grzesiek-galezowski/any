using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.Generic;

public class GeneratorByPickingFromSpecifiedSetOfValues<T> : InlineGenerator<T>
{
  private static readonly LatestArraysWithPossibleValues<T> _cachedArraysOfCurrentType = new();
  private readonly T[] _possibleValues;

  public GeneratorByPickingFromSpecifiedSetOfValues(T[] possibleValues)
  {
    _possibleValues = possibleValues;
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    if (!_cachedArraysOfCurrentType.Contain(_possibleValues))
    {
      _cachedArraysOfCurrentType.Add(_possibleValues);
    }

    return _cachedArraysOfCurrentType.PickNextElementFrom(_possibleValues);
  }
}