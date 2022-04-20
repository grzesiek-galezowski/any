using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;

namespace TddXt.AnyGenerators.AutoFixtureWrapper;

public class EmptyImmutableCollectionRelay : ISpecimenBuilder
{
  public object Create(object request, ISpecimenContext context)
  {
    if (context == null) throw new ArgumentNullException(nameof(context));

    if (request is Type t)
    {
      if (t.IsGenericType)
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
          var emptyField = t.GetField("Empty", BindingFlags.Public | BindingFlags.Static);
          return emptyField.GetValue(null);

        }
        else if (new[]
                 {
                   typeof(ImmutableStack<>),
                   typeof(ImmutableQueue<>)
                 }.Contains(t.GetGenericTypeDefinition()))
        {
          var emptyProperty = t.GetProperty("Empty", BindingFlags.Public | BindingFlags.Static);
          return emptyProperty.GetValue(null);
        }
      }
    }

    return new NoSpecimen();
  }
}