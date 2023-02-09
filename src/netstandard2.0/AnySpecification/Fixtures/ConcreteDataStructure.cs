using System;
using System.Collections.Immutable;

namespace AnySpecification.Fixtures;

public class ConcreteDataStructure
{
  public ConcreteDataStructure2 _field;
  public TimeSpan Span { get; set; }
  public ConcreteDataStructure2 Data { get; set; }
  public ImmutableList<int> AnImmutableList { get; set; } = ImmutableList<int>.Empty;
}
