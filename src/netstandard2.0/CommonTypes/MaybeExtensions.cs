using System.Collections.Generic;
using System.Linq;
using TddXt.CommonTypes;

namespace TddXt.AnyGenerators.Generic
{
  public static class MaybeExtensions
  {
    public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> enumerable) where T : class
    {
      if (enumerable.Any())
      {
        return Maybe.Just(enumerable.First());
      }
      else
      {
        return Maybe<T>.Not;
      }
    }
  }
}