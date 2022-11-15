using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;
using Core.NullableReferenceTypesExtensions;

namespace TddXt.TypeResolution.FakeChainElements.InlineGeneratorTypes.AutoFixtureWrapper;

public class EmptyImmutableCollectionRelay : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (context == null) throw new ArgumentNullException(nameof(context));

    if (request is Type { IsGenericType: true } t)
    {
      if (new []
          {
            typeof(ImmutableArray<>), 
            typeof(ImmutableList<>),
            typeof(ImmutableDictionary<,>),
            typeof(ImmutableHashSet<>),
            typeof(ImmutableSortedDictionary<,>),
            typeof(ImmutableSortedSet<>),
          }.Contains(t.GetGenericTypeDefinition()))
      {
        var emptyField = t
          .GetField("Empty", BindingFlags.Public | BindingFlags.Static)
          .OrThrow();
        return emptyField
          .GetValue(null)
          .OrThrow();

      }
      else if (new[]
               {
                 typeof(ImmutableStack<>),
                 typeof(ImmutableQueue<>)
               }.Contains(t.GetGenericTypeDefinition()))
      {
        var emptyProperty = t
          .GetProperty("Empty", BindingFlags.Public | BindingFlags.Static)
          .OrThrow();
        return emptyProperty
          .GetValue(null)
          .OrThrow();
      }
    }

    return new NoSpecimen();
  }
}
