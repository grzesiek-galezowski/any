namespace AnySpecification.Fixtures;

public interface ISettable<T>
{
  T Value { set; }
}