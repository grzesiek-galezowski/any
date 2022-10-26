namespace AnySpecification.Fixtures;

public class ObjectWrappingObjectWithGenericOption<T>
{
  public ObjectWrappingObjectWithGenericOption(ObjectWithGenericOption<T> obj)
  {
    Obj = obj;
  }

  public ObjectWithGenericOption<T> Obj { get; }
}