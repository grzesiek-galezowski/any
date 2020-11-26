using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class EnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
  {
    private readonly int _length;

    public EnumerableGenerator(int length)
    {
      _length = length;
    }

    public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      //todo create empty collection factory to be able to use object here
      return GeneratedCollectionItems.AddTo(new List<T>(), _length, instanceGenerator, trace);
    }
  }
}