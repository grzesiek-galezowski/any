using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.AutoFixtureWrapper;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.Root.ImplementationDetails;

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
      .Prepend(new EnumGenerator());
  }

  public object Resolve(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    foreach (var defaultPrimitiveBuilder in _defaultPrimitiveBuilders)
    {
      var specimen = defaultPrimitiveBuilder.Create(type, new InvalidContext(defaultPrimitiveBuilder));
      if (specimen is not NoSpecimen)
      {
        return specimen;
      }
    }

    return _next.Resolve(instanceGenerator, request, type);

  }
}
