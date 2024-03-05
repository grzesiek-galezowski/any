namespace AnySpecification.Fixtures;

public class RecursiveClass
{
  public RecursiveClass? Same { get; set; }
  public string? Whatever { get; set; }
  public RecursiveClass2? Other { get; set; }
  public RecursiveClass2[]? Others { get; set; }
}

public class ObjectWithIndirectRecursion
{
  public ObjectWithIndirectRecursion2? Other2 { get; set; }
}

public class ObjectWithIndirectRecursion2
{
  public ObjectWithIndirectRecursion? Other { get; set; }
}

public class RecursiveClassWithOnlyReferenceToItself
{
  public RecursiveClassWithOnlyReferenceToItself? Same { get; set; }
}

public class RecursiveClassWithOnlyReferenceToItselfInConstructor
{
  public RecursiveClassWithOnlyReferenceToItselfInConstructor(RecursiveClassWithOnlyReferenceToItselfInConstructor same)
  {
    Same = same;
  }

  public RecursiveClassWithOnlyReferenceToItselfInConstructor Same { get; }
}
