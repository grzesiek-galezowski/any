using System;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.AutoFixtureWrapper;

public class CustomizationRelay : ISpecimenBuilder
{
  private readonly InstanceGenerator _gen;
  private readonly GenerationRequest _request;

  public CustomizationRelay(InstanceGenerator gen, GenerationRequest request)
  {
    _gen = gen;
    _request = request;
  }

  public object Create(object request, ISpecimenContext context)
  {
    if (context == null) throw new ArgumentNullException(nameof(context));

    if (request is Type t)
    {
      foreach (var customization in _request.GenerationCustomizations)
      {
        if (customization.AppliesTo(t))
        {
          return customization.Generate(t, _gen, _request);
        }
      }
      return new NoSpecimen();
    }

    return new NoSpecimen();
  }
}