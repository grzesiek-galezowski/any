using System;

namespace AnySpecification.Fixtures
{
  [Serializable]
  public class ObjectWithCopyConstructor
  {
    internal string _field;

    public ObjectWithCopyConstructor(ObjectWithCopyConstructor o)
    {
      _field = o._field;
    }
  }
}