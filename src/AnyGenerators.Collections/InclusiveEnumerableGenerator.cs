using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class InclusiveEnumerableGenerator<T> : InlineGenerator<IEnumerable<T>>
  {
    private readonly IEnumerable<T> _included;

    public InclusiveEnumerableGenerator(IEnumerable<T> included)
    {
      _included = included;
    }

    public IEnumerable<T> GenerateInstance(InstanceGenerator instanceGenerator)
    {
      var list = new List<T>();
      list.Add(instanceGenerator.Instance<T>());
      list.AddRange(_included);
      list.Add(instanceGenerator.Instance<T>());

      return list;
    }
  }
}