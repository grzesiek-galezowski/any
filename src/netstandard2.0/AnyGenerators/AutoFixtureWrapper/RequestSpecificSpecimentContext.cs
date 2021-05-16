using System;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  public class RequestSpecificSpecimentContext : ISpecimenContext
  {
    private readonly SpecimenContext _specimenContext;
    private readonly InstanceGenerator _instanceGenerator;
    private readonly GenerationRequest _generationRequest;

    public RequestSpecificSpecimentContext(SpecimenContext specimenContext, InstanceGenerator instanceGenerator,
      GenerationRequest generationRequest)
    {
      _specimenContext = specimenContext;
      _instanceGenerator = instanceGenerator;
      _generationRequest = generationRequest;
    }

    public object Resolve(object request)
    {
      if (request is Type t)
      {
        if (typeof(GenerationRequest).IsAssignableFrom(t))
        {
          return _generationRequest;
        }
        if (typeof(InstanceGenerator).IsAssignableFrom(t))
        {
          return _instanceGenerator;
        }
      }

      return _specimenContext.Resolve(request);
    }
  }
}
