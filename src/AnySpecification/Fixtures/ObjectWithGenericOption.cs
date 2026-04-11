using Optional;

namespace AnySpecification.Fixtures;

public class ObjectWithGenericOption<T>
{
  public ObjectWithGenericOption(Option<T> myOption)
  {
    MyOption = myOption;
  }

  public Option<T> MyOption { get; }
}