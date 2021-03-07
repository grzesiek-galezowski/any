using AutoFixture;
using System;
using TddXt.AnyExtensibility;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.AutoFixtureWrapper
{
  [Serializable]
  public class FixtureWrapper
  {
    private readonly Fixture _autoFixture;

    public FixtureWrapper(Fixture autoFixture)
    {
      _autoFixture = autoFixture;
    }

    public T Create<T>()
    {
      try
      {
        return _autoFixture.Create<T>();
      }
      catch (ObjectCreationException e)
      {
        throw new ThirdPartyGeneratorFailed(e);
      }
    }

    public T Create<T>(T seed)
    {
      try
      {
        return _autoFixture.Create(seed);
      }
      catch (ObjectCreationException e)
      {
        throw new ThirdPartyGeneratorFailed(e);
      }
    }

    public void Register<T>(Func<T> source)
    {
      _autoFixture.Register(source);
    }

    public static FixtureWrapper CreateUnconfiguredInstance()
    {
      var fixture = new Fixture(new EngineWithReplacedQuery());
      var fixtureWrapper = new FixtureWrapper(fixture);
      return fixtureWrapper;
    }

    public static FixtureWrapper InstanceForEmptyCollections()
    {
      return new FixtureWrapper(new Fixture
      {
        RepeatCount = 0
      });
    }

    public IDisposable CustomizeWith(GenerationCustomization[] customizations, InstanceGenerator gen,
      GenerationRequest request)
    {
      return new CustomizationScope(_autoFixture, customizations, gen, request);
    }
  }
}