using System;
#if NETFRAMEWORK
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
#else
using AutoFixture;
using AutoFixture.Kernel;
#endif
using TddXt.AnyExtensibility;
using TddXt.CommonTypes;

namespace TddXt.AutoFixtureWrapper
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
        GenerationTrace trace)
      {
        return new CustomizationScope(_autoFixture, customizations, gen, trace);
      }
    }

    public class CustomizationScope : IDisposable
    {
      private readonly Fixture _generator;

      public CustomizationScope(
        Fixture generator, 
        GenerationCustomization[] customizations, 
        InstanceGenerator gen,
        GenerationTrace trace)
      {
        _generator = generator;
        generator.Customizations.Insert(0, new CustomizationRelay(customizations, gen, trace));
      }

      public void Dispose()
      {
        _generator.Customizations.RemoveAt(0);
      }
    }

    public class CustomizationRelay : ISpecimenBuilder
    {
      private readonly GenerationCustomization[] _customizations;
      private readonly InstanceGenerator _gen;
      private readonly GenerationTrace _trace;

      public CustomizationRelay(GenerationCustomization[] customizations, InstanceGenerator gen, GenerationTrace trace)
      {
        _customizations = customizations;
        _gen = gen;
        _trace = trace;
      }

      public object Create(object request, ISpecimenContext context)
      {
        if (context == null) throw new ArgumentNullException(nameof(context));

        if (request is Type t)
        {
          foreach (var customization in _customizations)
          {
            if (customization.AppliesTo(t))
            {
              return customization.Generate(t, _gen, _trace);
            }
          }
          return new NoSpecimen();
        }

        return new NoSpecimen();
      }
    }
}