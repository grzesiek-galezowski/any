using System;
using System.Threading;
using AutoFixture;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  public class CustomizationScope : IDisposable
  {
    private readonly Fixture _generator;
    private readonly object _syncRoot;
    private readonly GenerationRequest _request;

    public CustomizationScope(
      Fixture generator,
      InstanceGenerator gen,
      GenerationRequest request, 
      object syncRoot)
    {
      _syncRoot = syncRoot;
      _generator = generator;
      _request = request;
      if (AnyCustomizationsIn(request))
      {
        ApplyCustomizations(generator, gen, request);
      }
    }

    private void ApplyCustomizations(IFixture generator, InstanceGenerator gen, GenerationRequest request)
    {
      Monitor.Enter(_syncRoot);
      try
      {
        generator.Customizations.Insert(0, new CustomizationRelay(gen, request));
      }
      catch
      {
        Monitor.Exit(_syncRoot);
        throw;
      }
    }

    public void Dispose()
    {
      if (AnyCustomizationsIn(_request))
      {
        RemoveCustomizations();
      }
    }

    private bool AnyCustomizationsIn(GenerationRequest generationRequest)
    {
      return generationRequest.GenerationCustomizations.Length > 0;
    }

    private void RemoveCustomizations()
    {
      try
      {
        _generator.Customizations.RemoveAt(0);
      }
      finally
      {
        Monitor.Exit(_syncRoot);
      }
    }
  }
}
