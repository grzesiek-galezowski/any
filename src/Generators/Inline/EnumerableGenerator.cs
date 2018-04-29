using System.Collections.Generic;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class EnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
  {
    private readonly int _length;

    public EnumerableGenerator(int length)
    {
      _length = length;
    }

    public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator)
    {
      //todo create empty collection factory to be able to use object here
      return CollectionFiller.FillingCollection(new List<T>(), _length, instanceGenerator);
    }

  }
}