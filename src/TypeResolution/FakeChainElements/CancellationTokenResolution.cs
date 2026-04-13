using System;
using System.Threading;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements
{
  public class CancellationTokenResolution : IResolution
  {
    public bool AppliesTo(Type type) => type == typeof(CancellationToken);

    public object? Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
    {
      using var cts = new CancellationTokenSource();
      cts.Cancel();
      var cancellationToken = cts.Token;
      return cancellationToken;
    }
  }
}
