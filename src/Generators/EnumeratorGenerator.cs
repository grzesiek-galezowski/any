using System.Collections.Generic;
using TddEbook.TddToolkit.Generators;
using TddEbook.TypeReflection;

namespace Generators
{
  public class EnumeratorGenerator<T> : InlineGenerator<IEnumerator<T>>
  {
    public IEnumerator<T> GenerateInstance(IInstanceGenerator instanceGenerator)
    {
      return new EnumerableGenerator<T>(AllGenerator.Many)
        .AsList().GenerateInstance(instanceGenerator).GetEnumerator();
    }
  }
}