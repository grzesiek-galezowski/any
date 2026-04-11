namespace AnySpecification.Fixtures;

public class ObjectWithInConstructorParameter
{
  public readonly int A;

  public ObjectWithInConstructorParameter(in int a)
  {
    A = a;
  }
}