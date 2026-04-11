using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

public class HalfResolution : IResolution
{
  private static readonly object SyncRoot = new();
  private static Half _current;
  public bool AppliesTo(Type type)
  {
    return type == typeof(Half);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    lock (SyncRoot)
    {
      _current = (Half)((float)_current + (float)Half.Epsilon);
      return _current;
    }
  }
}
