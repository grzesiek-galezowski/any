using System;
using AutoFixture.Kernel;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

//because we don't want AutoFixture delegating to itself as it loses all request context
//like recursion limit etc.
public class InvalidContext(ISpecimenBuilder specimenBuilder) : ISpecimenContext
{
  public object? Resolve(object request)
  {
    throw new InvalidOperationException("NOOOOO! " + request + " " + specimenBuilder);
  }
}
