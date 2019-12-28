namespace AnySpecification.Fixtures
{
  public interface IGetSettable<T>
  {
    T Value { get; set; }
  }
}