using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class ExclusiveEnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
  {
    private readonly T[] _excluded;

    public ExclusiveEnumerableGenerator(T[] excluded)
    {
      _excluded = excluded;
    }

    public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var result = new List<T>
      {
        instanceGenerator.OtherThan(_excluded),
        instanceGenerator.OtherThan(_excluded),
        instanceGenerator.OtherThan(_excluded)
        //todo honor MANY here
      };
      return result;
    }

  }
}