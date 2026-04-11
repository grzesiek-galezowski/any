using System.Collections.Generic;

namespace AnySpecification.Fixtures;

public class ObjectWithGenericCollection<T>
{
  public ObjectWithGenericCollection(List<T> myList)
  {
    MyList = myList;
  }

  public List<T> MyList { get; }
}