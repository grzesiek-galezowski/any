using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class GeneratorByPickingFromSpecifiedSetOfValues<T> : InlineGenerator<T>
{
  private static readonly LatestArraysWithPossibleValues<T> CachedArraysOfCurrentType = new();
  private readonly T[] _possibleValues;

  public GeneratorByPickingFromSpecifiedSetOfValues(T[] possibleValues)
  {
    _possibleValues = possibleValues;
  }

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    lock (CachedArraysOfCurrentType)
    {
      if (!CachedArraysOfCurrentType.Contain(_possibleValues))
      {
        CachedArraysOfCurrentType.Add(_possibleValues);
      }

      return CachedArraysOfCurrentType.PickNextElementFrom(_possibleValues);
    }
  }
}
