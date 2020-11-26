using TddXt.AnyExtensibility;
using TddXt.TypeResolution.CustomCollections;

namespace TddXt.AnyGenerators.Generic
{
  public class GeneratorByPickingFromSpecifiedSetOfValues<T> : InlineGenerator<T>
  {
    //private static readonly ArrayElementPicking _arrayElementPicking = new ArrayElementPicking();
    private static readonly LatestArraysWithPossibleValues<T> _cachedArraysOfCurrentType = new LatestArraysWithPossibleValues<T>();
    private readonly T[] _possibleValues;


    public GeneratorByPickingFromSpecifiedSetOfValues(T[] possibleValues)
    {
      _possibleValues = possibleValues;
    }

    public T GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      if (!_cachedArraysOfCurrentType.Contain(_possibleValues))
      {
        _cachedArraysOfCurrentType.Add(_possibleValues);
      }

      return _cachedArraysOfCurrentType.PickNextElementFrom(_possibleValues);
    }
  }
}