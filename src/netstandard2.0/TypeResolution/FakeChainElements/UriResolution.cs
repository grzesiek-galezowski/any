using System;
using AutoFixture;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class UriResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return type == typeof(Uri);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    var authority = instanceGenerator.Instance<string>(request);
    var scheme = instanceGenerator.Instance<UriScheme>(request);

    return MakeUri(scheme, authority);
  }

  private static Uri MakeUri(UriScheme scheme, string authority) => new Uri(scheme?.ToString() + "://" + authority);
}
