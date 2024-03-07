using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

namespace TddXt.TypeResolution.ResolutionChaining;

public class AutoFixtureChain : IGenerationChain
{
  private readonly IGenerationChain _next;
  private readonly IEnumerable<ISpecimenBuilder> _defaultPrimitiveBuilders;

  public AutoFixtureChain(IGenerationChain next)
  {
    _next = next;
    _defaultPrimitiveBuilders = new DefaultPrimitiveBuilders()
      .Where(b => b is not (
        TypeGenerator or
        DelegateGenerator or
        TaskGenerator or
        UriGenerator or
        MailAddressGenerator or
        EmailAddressLocalPartGenerator or
        DomainNameGenerator or
        StringSeedRelay))
      .Prepend(new EnumGenerator())
      .Prepend(new ByteSequenceGenerator()) // the random number gen can sometimes generate non-distinct values
      .Prepend(new SByteSequenceGenerator()) // the random number gen can sometimes generate non-distinct values
      .ToList(); //ToList() is necessary to pin down the specific instances of objects
  }

  public object? Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var builder in _defaultPrimitiveBuilders)
    {
      var specimen = builder.Create(type, new InvalidContext(builder));
      if (specimen is not NoSpecimen)
      {
        request.Trace.SelectedResolution(type, builder);
        return specimen;
      }
    }

    return _next.Resolve(instanceGenerator, request, type);
  }
}
