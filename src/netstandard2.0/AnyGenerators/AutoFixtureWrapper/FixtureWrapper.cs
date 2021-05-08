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
    private readonly object _syncRoot = new object();

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
      var autoFixture = new Fixture
      {
        RepeatCount = 0
      };
      autoFixture.Customizations.Add(new EmptyImmutableCollectionRelay());
      var instanceForEmptyCollections = new FixtureWrapper(autoFixture);
      return instanceForEmptyCollections;
    }

    public IDisposable CustomizeWith(GenerationCustomization[] customizations, InstanceGenerator gen,
      GenerationRequest request)
    {
      return new CustomizationScope(_autoFixture, customizations, gen, request, _syncRoot);
    }
  }
}