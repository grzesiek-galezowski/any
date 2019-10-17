using System;

namespace TddToolkitSpecification.Fixtures
{
  [Serializable]
  public abstract class AbstractObjectWithInterfaceInConstructor
  {
    private readonly int _a;
    private readonly string _b;
    public readonly ISimple _constructorArgument;
    public readonly ObjectWithInterfaceInConstructor _constructorNestedArgument;

#pragma warning disable CC0060 // Abastract class should not have public constructors.
    public AbstractObjectWithInterfaceInConstructor(
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
#pragma warning restore CC0060 // Abastract class should not have public constructors.

    public abstract int AbstractInt { get; }

    public int SettableInt { get; set; }
  }
}