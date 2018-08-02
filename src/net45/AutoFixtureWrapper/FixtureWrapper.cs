using System;
using Ploeh.AutoFixture;
using TddXt.CommonTypes;

namespace TddXt.AutoFixtureWrapper
{
    [Serializable]
    public class FixtureWrapper 
    {
        private readonly Fixture _generator;

        public FixtureWrapper(Fixture generator)
        {
            _generator = generator;
        }

        public T Create<T>()
        {
            try
            {
                return _generator.Create<T>();
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
                return _generator.Create(seed);
            }
            catch (ObjectCreationException e)
            {
                throw new ThirdPartyGeneratorFailed(e);
            }
        }

        public void Register<T>(Func<T> source)
        {
            _generator.Register(source);
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
    }
}