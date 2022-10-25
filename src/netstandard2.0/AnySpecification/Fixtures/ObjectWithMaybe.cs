using System;
using Core.Maybe;

namespace AnySpecification.Fixtures;

[Serializable]
public class ObjectWithMaybe
{
  public Maybe<ObjectWithMaybe> _field;
  public Maybe<ObjectWithMaybe> Property { get; set; }
}
