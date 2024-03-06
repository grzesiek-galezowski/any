using System.Diagnostics.CodeAnalysis;

namespace AnySpecification.Fixtures;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Feature
{
  public IGeometry? Geometry { get; set; }
}
