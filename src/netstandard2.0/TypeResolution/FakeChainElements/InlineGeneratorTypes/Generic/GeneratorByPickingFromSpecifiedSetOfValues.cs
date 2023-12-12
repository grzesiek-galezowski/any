using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.Generic;

public class GeneratorByPickingFromSpecifiedSetOfValues<T>(T[] possibleValues) : InlineGenerator<T>
{
  private static readonly LatestArraysWithPossibleValues<T> CachedArraysOfCurrentType = new();

  public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationRequest request)
  {
    lock (CachedArraysOfCurrentType)
    {
      if (!CachedArraysOfCurrentType.Contain(possibleValues))
      {
        CachedArraysOfCurrentType.Add(possibleValues);
      }

      return CachedArraysOfCurrentType.PickNextElementFrom(possibleValues);
    }
  }
}
