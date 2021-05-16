using AutoFixture;
using System;
using AutoFixture.Kernel;
using TddXt.AnyExtensibility;
using TddXt.AnyGenerators.Root.ImplementationDetails;
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

    public T Create<T>(InstanceGenerator gen, GenerationRequest request)
    {
      return (T)Create(typeof(T), gen, request);
    }

    public string CreateString()
    {
      return _autoFixture.Create<string>();
    }

    public byte CreateByte()
    {
      return _autoFixture.Create<byte>();
    }

    public object Create(Type type, InstanceGenerator instanceGenerator, GenerationRequest generationRequest)
    {
      try
      {
        return _autoFixture.Create(type, 
          new RequestSpecificSpecimentContext(
            new SpecimenContext(_autoFixture), instanceGenerator, generationRequest));
      }
      catch (ObjectCreationException e)
      {
        throw new ThirdPartyGeneratorFailed(e);
      }
    }

    public object Create(Type type)
    {
      try
      {
        return _autoFixture.Create(type, new SpecimenContext(_autoFixture));
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
      //BUG!!!!!!!!!!
      fixture.Customizations.Insert(0, new ResolutionRelay(new FakeTypedTask()));
      //BUG!!!!!!!!!!
      var fixtureWrapper = new FixtureWrapper(fixture);
      return fixtureWrapper;
    }

    public static FixtureWrapper InstanceForEmptyCollections()
    {
      var autoFixture = new Fixture {RepeatCount = 0};
      autoFixture.Customizations.Add(new EmptyImmutableCollectionRelay());
      var instanceForEmptyCollections = new FixtureWrapper(autoFixture);
      return instanceForEmptyCollections;
    }

    public IDisposable Customize(GenerationRequest request, InstanceGenerator gen)
    {
      return new CustomizationScope(_autoFixture, gen, request, _syncRoot);
    }
  }
}
