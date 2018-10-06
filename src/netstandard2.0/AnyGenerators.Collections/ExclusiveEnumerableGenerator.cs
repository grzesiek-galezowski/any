using System.Collections.Generic;
using TddXt.AnyExtensibility;
using static System.Linq.Enumerable;

namespace TddXt.AnyGenerators.Collections
{
  public class ExclusiveEnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
  {
    private readonly T[] _excluded;
    private readonly int _many;

    public ExclusiveEnumerableGenerator(T[] excluded, int many)
    {
      _excluded = excluded;
      _many = many;
    }

    public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return Range(0, _many).Select(i => instanceGenerator.OtherThan(_excluded)).ToList();
    }
  }
}