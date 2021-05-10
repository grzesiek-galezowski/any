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

    public CustomizationScope(
      Fixture generator,
      InstanceGenerator gen,
      GenerationRequest request, 
      object syncRoot)
    {
      _syncRoot = syncRoot;
      Monitor.Enter(_syncRoot);
      try
      {
        _generator = generator;
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