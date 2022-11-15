using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;
#if NET5_0_OR_GREATER
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
#else
public class HalfResolution : IResolution
{
  public bool AppliesTo(Type type)
  {
    return false;
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    throw new NotSupportedException("Supported in .NET 6 or higher");
  }
}
#endif

