﻿using AutoFixture;
using System;
using AutoFixture.Kernel;
using TddXt.TypeResolution.FakeChainElements;

namespace TddXt.AnyGenerators.AutoFixtureWrapper;

[Serializable]
public class EmptyCollectionFixtureWrapper
{
  private readonly Fixture _autoFixture;

  private EmptyCollectionFixtureWrapper(Fixture autoFixture)
  {
    _autoFixture = autoFixture;
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

  public static EmptyCollectionFixtureWrapper InstanceForEmptyCollections()
  {
    var autoFixture = new Fixture {RepeatCount = 0};
    autoFixture.Customizations.Add(new EmptyImmutableCollectionRelay());
    var instanceForEmptyCollections = new EmptyCollectionFixtureWrapper(autoFixture);
    return instanceForEmptyCollections;
  }
}
