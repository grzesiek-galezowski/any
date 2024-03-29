﻿using System;
using TddXt.AnyExtensibility;

namespace TddXt.TypeResolution.FakeChainElements;

#if NET6_0_OR_GREATER
public class DateOnlyResolution : IResolution
{
  private static DateOnly _current = DateOnly.FromDateTime(DateTime.UtcNow);
  private static readonly object SyncRoot = new();
  public bool AppliesTo(Type type)
  {
    return type == typeof(DateOnly);
  }

  public object Apply(InstanceGenerator instanceGenerator, GenerationRequest request, Type type)
  {
    lock(SyncRoot)
    {
      _current = _current.AddDays(1);
      return _current;
    }
  }
}
#else
public class DateOnlyResolution : IResolution
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
