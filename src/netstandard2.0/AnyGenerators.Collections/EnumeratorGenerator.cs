using System.Collections.Generic;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.Collections
{
  public class EnumeratorGenerator<T> : InlineGenerator<IEnumerator<T>>
  {
    public IEnumerator<T> GenerateInstance(InstanceGenerator instanceGenerator, GenerationTrace trace)
    {
      return new EnumerableGenerator<T>(Configuration.Many)
        .AsList<T>().GenerateInstance(instanceGenerator, trace).GetEnumerator();
    }
  }
}