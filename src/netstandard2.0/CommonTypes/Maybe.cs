using System;

namespace TddXt.CommonTypes
{
  public static class Maybe
  {
    public static Maybe<T> Wrap<T>(T instance) where T : class
    {
      return new Maybe<T>(instance);
    }
  }

  public struct Maybe<T> where T : class
  {
    private readonly T _value;

    public Maybe(T instance)
      : this()
    {
      if (instance != null)
      {
        HasValue = true;
        _value = instance;
      }
    }

    public bool HasValue { get; }

    public T Value()
    {
      if (HasValue)
      {
        return _value;
      }
      else
      {
        throw new Exception("No instance of type " + typeof(T));
      }
    }

    public static Maybe<T> Not { get; } = new Maybe<T>();

    public T ValueOr(T alternativeValue)
    {
      return HasValue ? Value() : alternativeValue;
    }

    public Maybe<T> Otherwise(Maybe<T> alternative)
    {
      return HasValue ? this : alternative;
    }

    public static implicit operator Maybe<T>(T instance)
    {
      return Maybe.Wrap(instance);
    }

    public override string ToString()
    {
      return HasValue ? Value().ToString() : "<Nothing>";
    }
  }

}
