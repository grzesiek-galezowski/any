using System;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  class ResolutionRelay : ISpecimenBuilder
  {
    private readonly IResolution _resolution;

    public ResolutionRelay(IResolution resolution)
    {
      _resolution = resolution;
    }

    public object Create(object request, ISpecimenContext context)
    {
      if (request is Type type)
      {
        if (_resolution.AppliesTo(type))
        {
          var genRequest = (GenerationRequest)context.Resolve(typeof(GenerationRequest));
          var generator = (InstanceGenerator)context.Resolve(typeof(InstanceGenerator));
          return _resolution.Apply(generator, genRequest, type);
        }
      }

      return new NoSpecimen();
    }
  }
}
