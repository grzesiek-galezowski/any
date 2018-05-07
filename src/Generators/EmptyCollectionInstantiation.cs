using System;
using System.Collections.Generic;
using System.Linq;

namespace TddEbook.TddToolkit.Generators
{
  public class EmptyCollectionInstantiation
  {
    public object EmptyEnumerableOf(Type collectionItemType)
    {
      return new GenericMethodProxyCalls().ResultOfGenericVersionOfStaticMethod<MyEnumerable>(
        collectionItemType,
        nameof(MyEnumerable.Empty));
    }
    public class MyEnumerable
    {
      public static IEnumerable<T> Empty<T>()
      {
        return Enumerable.Empty<T>();
      }
    }
  }

}