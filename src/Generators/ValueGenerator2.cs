using AutoFixture;

namespace Generators
{
  public class ValueGenerator2<T> //todo
  {
    private readonly Fixture _generator;

    public ValueGenerator2(Fixture generator)
    {
      _generator = generator;
    }

    public T GenerateInstance()
    {
//bug: add support for creating generic structs with interfaces
      return _generator.Create<T>();
    }
  }
}