using System;

namespace TddToolkitSpecification.Fixtures
{
  [Serializable]
  public class ObjectWithInterfaceInConstructor
  {
    private readonly int _a;
    private readonly string _b;
    public readonly ISimple _constructorArgument;
    public readonly ObjectWithInterfaceInConstructor _constructorNestedArgument;

    public ObjectWithInterfaceInConstructor(
      int a,
      ISimple constructorArgument,
      string b,
      ObjectWithInterfaceInConstructor constructorNestedArgument)
    {
      _a = a;
      _constructorArgument = constructorArgument;
      _b = b;
      _constructorNestedArgument = constructorNestedArgument;
    }
  }
}