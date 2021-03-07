using System;
using System.Threading;
using AutoFixture;
using TddXt.AnyExtensibility;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  public class CustomizationScope : IDisposable
  {
    private readonly Fixture _generator;
    private readonly object _syncRoot = new();

    public CustomizationScope(
      Fixture generator,
      GenerationCustomization[] customizations,
      InstanceGenerator gen,
      GenerationRequest request)
    {
      Monitor.Enter(_syncRoot);
      try
      {
        _generator = generator;
        generator.Customizations.Insert(0, new CustomizationRelay(customizations, gen, request));
      }
      catch
      {
        Monitor.Exit(_syncRoot);
        throw;
      }

    }

    public void Dispose()
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