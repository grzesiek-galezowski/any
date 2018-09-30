namespace TddToolkitSpecification.Fixtures
{
  public class ObjectWithThrowingDependency
  {
    private readonly ObjectWithThrowingDependency2 _tic;

    public ObjectWithThrowingDependency(ObjectWithThrowingDependency2 tic)
    {
      _tic = tic;
    }
  }

  public class ObjectWithThrowingDependency2
  {
    private readonly ObjectWithThrowingDependency3 _tic;

    public ObjectWithThrowingDependency2(ObjectWithThrowingDependency3 tic)
    {
      _tic = tic;
    }
  }

  public class ObjectWithThrowingDependency3
  {
    private readonly ThrowingInConstructor _tic;

    public ObjectWithThrowingDependency3(ThrowingInConstructor tic)
    {
      _tic = tic;
    }
  }
}