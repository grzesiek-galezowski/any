using System.Collections.Immutable;

namespace AnySpecification.Fixtures;

public record ObjectWithImmutableList(ImmutableList<int> Elements);