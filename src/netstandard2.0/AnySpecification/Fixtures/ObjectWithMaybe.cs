using System;
using Core.Maybe;

namespace AnySpecification.Fixtures;

[Serializable]
public class ObjectWithMaybe
{
  public Maybe<ObjectWithMaybe> _field;
  public Maybe<ObjectWithMaybe> Property { get; set; }
}

[Serializable]
public class ObjectWithGettableMaybe
{
  private Maybe<string> _field = Maybe<string>.Nothing;
  public Maybe<string> Property { get; } = Maybe<string>.Nothing;

  public Maybe<string> GetFieldValue() => _field;
}
