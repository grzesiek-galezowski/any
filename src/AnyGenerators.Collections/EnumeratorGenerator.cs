using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace Generators
{
  public class EnumeratorGenerator<T> : InlineGenerator<IEnumerator<T>>
  {
    public IEnumerator<T> GenerateInstance(InstanceGenerator instanceGenerator)
    {
      return new EnumerableGenerator<T>(Configuration.Many)
        .AsList<T>().GenerateInstance(instanceGenerator).GetEnumerator();
    }
    
  }
}