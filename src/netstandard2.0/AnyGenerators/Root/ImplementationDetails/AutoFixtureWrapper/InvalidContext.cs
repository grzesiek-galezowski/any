using System;
using AutoFixture.Kernel;

namespace TddXt.AnyGenerators.Root.ImplementationDetails.AutoFixtureWrapper;

//because we don't want AutoFixture delegating to itself as it loses all request context
//like recursion limit etc.
public class InvalidContext : ISpecimenContext
{
  private readonly ISpecimenBuilder _specimenBuilder;

  public InvalidContext(ISpecimenBuilder specimenBuilder)
  {
    _specimenBuilder = specimenBuilder;
  }

  public object? Resolve(object request)
  {
    throw new InvalidOperationException("NOOOOO! " + request + " " + _specimenBuilder);
  }
}
